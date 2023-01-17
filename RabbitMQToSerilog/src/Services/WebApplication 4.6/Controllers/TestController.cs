using Serilog;
using System.Web.Mvc;

namespace WebApplication_4._6.Controllers
{
    public class TestController : Controller
    {

        // GET: Test
        public ActionResult Index()
        {
            int a = 1; int b = 0;
            Log.Logger.Warning("F");

            return View();
        }

        public ActionResult Testing()
        {
            int a = 1; int b = 0;
            int c = a / b;
            return View();
        }
    }
}