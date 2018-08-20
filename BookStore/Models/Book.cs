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

        public string ISBN { get; set; }

        public int PageCount { get; set; }

        public int Year { get; set; }

        public bool Hardcover { get; set; }

        public Language Language { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }
    }
}