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
    public class BasketController : Controller
    {
        private ApplicationDbContext _context;

        public BasketController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult GetBasketContent()
        {
            var bookList = _context.Books.Include(b => b.Author).Where(b => b.AddedToBasket == true).ToList();

            var bookListView = new BasketView
            {
                BookList = bookList
            };

            return View(bookListView);
        }
    }
}