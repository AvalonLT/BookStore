using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace BookStore.Services
{
    public class MainControllerService
    {
        private ApplicationDbContext _context;

        public MainControllerService()
        {
            _context = new ApplicationDbContext();
        } 

        public List<Book> GetBookListAndPagination(string year, string pageCount, string hardcover, string language, int? pageNumber)
        {
            bool hardcoverBool = (hardcover == "Yes") ? true : false;
            int pageNumberInt = (pageNumber == null) ? 1 : (int)pageNumber;

            int yearStart = GetYearSplit(year)[0];
            int yearEnd = GetYearSplit(year)[1];
            int pageCountStart = GetPageCountSplit(pageCount)[0];
            int pageCountEnd = GetPageCountSplit(pageCount)[1];

            var bookList = _context.Books.Include(b => b.Author).Include(b => b.Language)
                .OrderByDescending(b => b.CreationDate)
                .Where(b => (hardcover == "") ? true : b.Hardcover == hardcoverBool)
                .Where(b => (language == "") ? true : b.Language.Name == language)
                .Where(b => (year == "" || yearStart == 0 || yearEnd == 0) ? true : b.Year >= yearStart && b.Year <= yearEnd)
                .Where(b => (pageCount == "" || pageCountStart == 0 || pageCountEnd == 0) ? true : b.PageCount >= pageCountStart && b.PageCount <= pageCountEnd)
                .Skip((pageNumberInt - 1) * 10).Take(10).ToList();

            return bookList;
        }

        public int GetBookListAndPaginationCount(string year, string pageCount, string hardcover, string language)
        {
            bool hardcoverBool = (hardcover == "Yes") ? true : false;

            int yearStart = GetYearSplit(year)[0];
            int yearEnd = GetYearSplit(year)[1];
            int pageCountStart = GetPageCountSplit(pageCount)[0];
            int pageCountEnd = GetPageCountSplit(pageCount)[1];

            var bookListCount = _context.Books.Include(b => b.Author).Include(b => b.Language)
                .OrderByDescending(b => b.CreationDate)
                .Where(b => (hardcover == "") ? true : b.Hardcover == hardcoverBool)
                .Where(b => (language == "") ? true : b.Language.Name == language)
                .Where(b => (year == "" || yearStart == 0 || yearEnd == 0) ? true : b.Year >= yearStart && b.Year <= yearEnd)
                .Where(b => (pageCount == "" || pageCountStart == 0 || pageCountEnd == 0) ? true : b.PageCount >= pageCountStart && b.PageCount <= pageCountEnd)
                .ToList().Count;

            return bookListCount;
        }

        public void SaveSearch(string year, string pageCount, string hardcover, string language)
        {
            var searchInput = new SearchInput
            {
                Year = year,
                PageCount = pageCount,
                Hardcover = hardcover,
                Language = language
            };

            _context.SearchInputs.Add(searchInput);
            _context.SaveChanges();
        }

        private int[] GetYearSplit(string year)
        {
            int yearStart, yearEnd;
            yearStart = yearEnd = 0;

            if (year != "")
            {
                string[] yearSplit = year.Split('-');
                yearStart = Int32.Parse(yearSplit[0]);
                yearEnd = Int32.Parse(yearSplit[1]);
            }

            int[] page = new int[] { yearStart, yearEnd };

            return page;
        }

        private int[] GetPageCountSplit(string pageCount)
        {
            int pageCountStart, pageCountEnd;
            pageCountStart = pageCountEnd = 0;

            if (pageCount != "")
            {
                string[] pageCountSplit = pageCount.Split('-');
                pageCountStart = Int32.Parse(pageCountSplit[0]);
                pageCountEnd = Int32.Parse(pageCountSplit[1]);
            }

            int[] page = new int[] { pageCountStart, pageCountEnd };

            return page;
        }
    }
}