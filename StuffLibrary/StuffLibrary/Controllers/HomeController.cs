using System.Web.Mvc;

namespace StuffLibrary.Controllers
{
    public class HomeController : StuffLibraryControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
