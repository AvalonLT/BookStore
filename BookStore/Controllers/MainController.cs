using BookStore.Models;
using System;
using System.Collections.Generic;
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
            var booksList = _context.Books.ToList();
            return View(booksList);
        }
    }
}