using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string ISBN { get; set; }

        public int PageCount { get; set; }

        public int Year { get; set; }

        public bool Hardcover { get; set; }

        public int LanguageId { get; set; }

        public int AuthorId { get; set; }
    }
}