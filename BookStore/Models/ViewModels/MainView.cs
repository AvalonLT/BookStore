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
    }
}