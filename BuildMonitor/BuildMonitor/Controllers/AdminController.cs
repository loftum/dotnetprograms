using System.Web.Mvc;
using BuildMonitor.Lib.Api;
using BuildMonitor.Models.Admin;

namespace BuildMonitor.Controllers
{
    public class AdminController : BuildMonitorControllerBase
    {
        private readonly IMonitorFacade _monitorFacade;

        public AdminController(IMonitorFacade monitorFacade)
        {
            _monitorFacade = monitorFacade;
        }

        public ActionResult Index()
        {
            return RedirectToAction("EditSettings");
        }

        public ActionResult EditSettings()
        {
            var config = _monitorFacade.GetConfiguration();
            var model = new EditSettingsViewModel(config);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSettings(EditSettingsViewModel model)
        {
            _monitorFacade.SaveConfiguration(model.Config);
            return RedirectToAction("EditSettings");
        }
    }
}