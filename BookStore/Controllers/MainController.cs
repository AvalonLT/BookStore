using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class MainController : Controller
    {
        private ApplicationDbContext _context;
        private MainControllerService _service;
        
        public MainController()
        {
            _context = new ApplicationDbContext();
            _service = new MainControllerService();
        }

        public ActionResult GetOnePageOfBooks(int? pageNumber)
        {
            int pageNumberInt = (pageNumber == null) ? 1 : (int)pageNumber;
            
            var mainViewBookList = new MainView
            {
                BookList = _context.Books.Include(b => b.Author).Include(b => b.Language)
                .OrderByDescending(b => b.CreationDate).Skip((pageNumberInt - 1) * 10).Take(10).ToList(),
                ListSize = _context.Books.ToList().Count,
                Languages = _context.Languages.ToList(),
                FilterOn = false
            };
            
            return View(mainViewBookList);
        }

        public ActionResult FilterBooks(string year, string pageCount, string hardcover, string language, int? pageNumber)
        {
            if (pageNumber == null)
            {
                _service.SaveSearch(year, pageCount, hardcover, language);
            }
            else
            {
                var lastSearchInput = _context.SearchInputs.OrderByDescending(s => s.Id).First();
                var bookListSearch = _service.GetBookListAndPagination(lastSearchInput.Year, lastSearchInput.PageCount, lastSearchInput.Hardcover, lastSearchInput.Language, pageNumber);

                var mainViewBookSearchList = new MainView
                {
                    BookList = bookListSearch,
                    ListSize = _service.GetBookListAndPaginationCount(lastSearchInput.Year, lastSearchInput.PageCount, lastSearchInput.Hardcover, lastSearchInput.Language),
                    Languages = _context.Languages.ToList(),
                    FilterOn = true,
                };

                return View("GetOnePageOfBooks", mainViewBookSearchList);
            }

            var bookList = _service.GetBookListAndPagination(year, pageCount, hardcover, language, pageNumber);

            var mainViewBookList = new MainView
            {
                BookList = bookList,
                ListSize = _service.GetBookListAndPaginationCount(year, pageCount, hardcover, language),
                Languages = _context.Languages.ToList(),
                FilterOn = true,
            };

            return View("GetOnePageOfBooks", mainViewBookList);
        }

        public ActionResult AddBook()
        {
            BookView bookView = new BookView
            {
                Languages = _context.Languages.ToList()
            };

            return View(bookView);
        }

        [HttpPost]
        public ActionResult CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                BookView bookView = new BookView
                {
                    Book = book,
                    Languages = _context.Languages.ToList()
                };
                return View("AddBook", bookView);
            }

            if (book.Id == 0) //create
            {
                _context.Books.Add(book);
            }
            else //update
            {
                var bookInDb = _context.Books.Include(b => b.Author).Single(b => b.Id == book.Id);

                bookInDb.Author.Name = book.Author.Name;
                bookInDb.Author.LastName = book.Author.LastName;
                bookInDb.Hardcover = book.Hardcover;
                bookInDb.ISBN = book.ISBN;
                bookInDb.Language = book.Language;
                bookInDb.LanguageId = book.LanguageId;
                bookInDb.PageCount = book.PageCount;
                bookInDb.Title = book.Title;
                bookInDb.Year = book.Year;

                UpdateModel(bookInDb);
            }

            _context.SaveChanges();
            return RedirectToAction("GetOnePageOfBooks");
        }

        public ActionResult EditBook(int id)
        {
            var book = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);

            if (book == null)
                HttpNotFound();

            BookView bookView = new BookView
            {
                Book = book,
                Languages = _context.Languages.ToList()
            };

            return View("AddBook", bookView);
        }
    }
}