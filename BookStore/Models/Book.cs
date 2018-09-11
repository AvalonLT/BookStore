using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Book
    {
        public Book()
        {
            CreationDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string ISBN { get; set; }

        public int PageCount { get; set; }

        public int Year { get; set; }

        public bool Hardcover { get; set; }

        public DateTime CreationDate { get; set; }

        public Language Language { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }


        public bool AddedToBasket { get; set; }
    }
}