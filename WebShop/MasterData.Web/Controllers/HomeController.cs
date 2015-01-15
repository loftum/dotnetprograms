using System.Web.Mvc;

namespace MasterData.Web.Controllers
{
    public class HomeController :MasterDataControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}