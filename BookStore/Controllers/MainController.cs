using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class MainController : Controller
    {
        // GET: Main/Test
        public ActionResult Test()
        {
            return View();
        }
    }
}