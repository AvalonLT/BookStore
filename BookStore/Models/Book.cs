using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public int PageCount { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public bool Hardcover { get; set; }

        [Required]
        public Language Language { get; set; }

        [Required]
        public int LanguageId { get; set; }

        [Required]
        public Author Author { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}