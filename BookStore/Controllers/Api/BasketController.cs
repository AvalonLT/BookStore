using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers.Api
{
    public class BasketController : ApiController
    {
        private ApplicationDbContext _context;

        public BasketController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPut]
        public IHttpActionResult AddToBasket(int id)
        {
            var book = _context.Books.Single(b => b.Id == id);

            if(book.AddedToBasket)
            {
                return Ok();
            }

            book.AddedToBasket = true;
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public void RemoveBook(int id)
        {
            var bookInDb = _context.Books.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            bookInDb.AddedToBasket = false;
            _context.SaveChanges();
        }
    }
}
