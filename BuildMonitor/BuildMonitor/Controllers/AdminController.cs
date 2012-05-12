using System.Linq;
using System.Web.Mvc;
using BuildMonitor.Lib.Api;
using BuildMonitor.Lib.Configuration;
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
            var availableProjects = _monitorFacade
                .GetAvailableProjectsFor(config.BuildServerConfig)
                .Select(project =>
                    new SelectListItem{Text = project.Name, Value = project.Id, Selected = config.BuildServerConfig.ProjectIds.Contains(project.Id)});
            model.AvailableProjects = availableProjects;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSettings(EditSettingsViewModel model)
        {
            _monitorFacade.SaveConfiguration(model.Config);
            return RedirectToAction("EditSettings");
        }

        [HttpPost]
        public ActionResult GetProjects(EditSettingsViewModel model)
        {
            var projects = _monitorFacade.GetAvailableProjectsFor(model.Config.BuildServerConfig);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
    }
}