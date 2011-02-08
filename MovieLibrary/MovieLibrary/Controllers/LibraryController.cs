using System.Web.Mvc;

namespace MovieLibrary.Controllers
{
    public class LibraryController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
