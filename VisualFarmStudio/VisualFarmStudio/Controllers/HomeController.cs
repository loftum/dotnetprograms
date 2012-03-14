using System.Web.Mvc;

namespace VisualFarmStudio.Controllers
{
    public class HomeController : VFSControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Bondegard");
        }
    }
}