using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models.ViewModels
{
    public class MainView
    {
        public List<Book> BookList { get; set; }
        public int ListSize { get; set; }
        public List<Language> Languages { get; set; }
        public bool FilterOn { get; set; }
        public int BasketSize { get; set; }
    }
}