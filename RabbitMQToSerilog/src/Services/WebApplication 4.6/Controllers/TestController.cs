using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication_4._6.Controllers
{
    public class TestController : Controller
    {

        // GET: Test
        public ActionResult Index()
        {
            int a = 1; int b = 0;
            int c = a/b; int d = b/a;
            return View();
        }
    }
}