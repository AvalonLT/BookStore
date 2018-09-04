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
                ListSize = _context.Books.ToList().Count,
                Languages = _context.Languages.ToList()
            };
            
            return View(mainViewBookList);
        }

        public ActionResult FilterBooks(string year, string pageCount, string hardcover, string language, int? pageNumber)
        {
            if (pageNumber == null)
            {
                var searchInput = new SearchInput
                {
                    Year = year,
                    PageCount = pageCount,
                    Hardcover = hardcover,
                    Language = language
                };

                _context.SearchInputs.Add(searchInput);
                _context.SaveChanges();
            }
            else
            {
                var lastSearchInput = _context.SearchInputs.Select(s => s.Id).Single();

                var searchInput = new SearchInput
                {
                    Year = year,
                    PageCount = pageCount,
                    Hardcover = hardcover,
                    Language = language
                };

                UpdateModel(searchInput);
            }




            bool hardcoverBool = (hardcover == "Yes") ? true : false;

            int yearStart, yearEnd, pageCountStart, pageCountEnd;
            yearStart = yearEnd = pageCountStart = pageCountEnd = 0;

            if (year != "" || year != null)
            {
                string[] yearSplit = year.Split('-');
                yearStart = Int32.Parse(yearSplit[0]);
                yearEnd = Int32.Parse(yearSplit[1]);
            }

            if (pageCount != "" || pageCount != null)
            { 
                string[] pageCountSplit = pageCount.Split('-');
                pageCountStart = Int32.Parse(pageCountSplit[0]);
                pageCountEnd = Int32.Parse(pageCountSplit[1]);
            }

            var bookList = _context.Books.Include(b => b.Author).Include(b => b.Language)
                .OrderByDescending(b => b.CreationDate)
                .Where(b => (hardcover == "") ? true : b.Hardcover == hardcoverBool)
                .Where(b => (language == "") ? true : b.Language.Name == language)
                .Where(b => (year == "" || yearStart == 0 || yearEnd == 0) ? true : b.Year >= yearStart && b.Year <= yearEnd)
                .Where(b => (pageCount == "" || pageCountStart == 0 || pageCountEnd == 0) ? true : b.PageCount >= pageCountStart && b.PageCount <= pageCountEnd).ToList();

            var mainViewBookList = new MainView
            {
                BookList = bookList,
                ListSize = bookList.Count,
                Languages = _context.Languages.ToList(),
                FilterOn = "true"
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