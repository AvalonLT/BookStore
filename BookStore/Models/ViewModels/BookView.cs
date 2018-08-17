using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models.ViewModels
{
    public class BookView
    {
        public Book Book { get; set; }
        public Language Language { get; set; }
    }
}