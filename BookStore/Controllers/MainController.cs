using BookStore.Models;
using BookStore.Models.ViewModels;
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
        
        public MainController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult GetOnePageOfBooks(int? pageNumber)
        {
            int pageNumberInt = (pageNumber == null) ? 1 : (int)pageNumber;

            var mainViewBookList = new MainView
            {
                BookList = _context.Books.Include(b => b.Author).Include(b => b.Language)
                .OrderByDescending(b => b.CreationDate).Skip((pageNumberInt - 1) * 10).Take(pageNumberInt * 10).ToList(),
                ListSize = _context.Books.ToList().Count
            };
            
            return View(mainViewBookList);
        }

        public ActionResult FilterBooks(string year, string pageCount, bool hardcover = false)
        {
            var mainViewBookList = new MainView
            {
                BookList = _context.Books.Include(b => b.Author).Include(b => b.Language)
                .OrderByDescending(b => b.CreationDate).Where(b => b.Hardcover == hardcover).ToList(),
                ListSize = _context.Books.ToList().Count
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
            return RedirectToAction("GetAllBooks");
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