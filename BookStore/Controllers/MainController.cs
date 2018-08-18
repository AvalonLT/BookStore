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
            if (book.Id == 0)
            {
                book.Language = _context.Languages.Single(b => b.Id == book.LanguageId);
                _context.Books.Add(book);
            }
            else
            {
                var bookInDb = _context.Books.SingleOrDefault(b => b.Id == book.Id);
                TryUpdateModel(bookInDb);
            }

            _context.SaveChanges();
            return RedirectToAction("GetAllBooks");
        }

        public ActionResult EditBook(int id)
        {
            var book = _context.Books.Include(b => b.Author).SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                HttpNotFound();
            }

            return View("AddBook", book);
        }
    }
}