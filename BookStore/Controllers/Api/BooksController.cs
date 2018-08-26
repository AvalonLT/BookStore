using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookStore.Controllers.Api
{
    public class BooksController : ApiController
    {
        private ApplicationDbContext _context;

        public BooksController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/books
        public IEnumerable<Book> GetBooks()
        {
            var a = _context.Books.Include(b => b.Author).Include(b => b.Language).ToList();
            return a;
        }

        //GET /api/Books
        public Book GetBook(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return book;
        }

        //POST /api/Books
        [HttpPost]
        public Book CreateBook (Book book)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return book;
        }

        //PUT /api/Books
        [HttpPut]
        public void UpdateBook(Book book, int id)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var bookInDb = _context.Books.SingleOrDefault(b => b.Id == id);

            if (bookInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            bookInDb.Author.Name = book.Author.Name;
            bookInDb.Author.LastName = book.Author.LastName;
            bookInDb.Hardcover = book.Hardcover;
            bookInDb.ISBN = book.ISBN;
            bookInDb.Language = book.Language;
            bookInDb.LanguageId = book.LanguageId;
            bookInDb.PageCount = book.PageCount;
            bookInDb.Title = book.Title;
            bookInDb.Year = book.Year;

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteBook(int id)
        {

        }
    }
}
