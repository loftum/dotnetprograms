using System.Web.Mvc;

namespace VisualFarmStudio.Controllers
{
    public class HomeController : VisualFarmControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Bondegard");
        }
    }
}