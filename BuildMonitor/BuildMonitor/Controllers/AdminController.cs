using System.Web.Mvc;
using BuildMonitor.Models.Admin;

namespace BuildMonitor.Controllers
{
    public class AdminController : BuildMonitorControllerBase
    {
        public ActionResult Index()
        {
            var model = new AdminIndexViewModel();
            return View(model);
        }
    }
}