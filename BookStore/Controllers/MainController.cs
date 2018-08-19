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

        public ActionResult GetAllBooks()
        {
            var booksList = _context.Books.Include(b => b.Author).Include(b => b.Language).ToList();
            return View(booksList);
        }

        public ActionResult AddBook()
        {
            var languages = _context.Languages.ToList();

            BookView bookView = new BookView
            {
                Languages = languages
            };
            return View(bookView);
        }

        public ActionResult CreateBook(Book book)
        {
            if (book.Id == 0) //create
            {
                book.Language = _context.Languages.Single(b => b.Id == book.LanguageId);
                _context.Books.Add(book);
            }
            else //update
            {
                var bookInDb = _context.Books.Include(b => b.Author).Include(b => b.Language).Single(b => b.Id == book.Id);
                book.Language = _context.Languages.SingleOrDefault(b => b.Id == book.LanguageId);
                book.Author.Id = book.AuthorId;
                var a = _context.Books.Single(b => b.Id == book.Id);
                book.Author.Books.Add(a);

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