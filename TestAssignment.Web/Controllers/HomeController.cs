using NLog;
using System.Web.Mvc;

namespace TestAssignment.Web.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            Logger.Trace("Someone just opened our page");
            return View();
        }

        public ActionResult AngularIndex()
        {
            Logger.Trace("Someone just opened our Angular page");
            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}