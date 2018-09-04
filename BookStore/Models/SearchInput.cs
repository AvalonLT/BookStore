using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class SearchInput
    {
        public int Id { get; set; }
        public string Year { get; set; }
        public string PageCount { get; set; }
        public string Hardcover { get; set; }
        public string Language { get; set; }
    }
}