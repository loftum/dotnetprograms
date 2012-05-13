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
            return RedirectToAction("EditBuildServer");
        }

        public ActionResult EditBuildServer()
        {
            var config = _monitorFacade.GetBuildServerConfig();
            var model = new EditBuildServerViewModel(config);
            var availableProjects = _monitorFacade
                .GetAvailableProjectsFor(config)
                .Select(project =>
                    new SelectListItem{Text = project.Name, Value = project.Id, Selected = config.ProjectIds.Contains(project.Id)});
            model.AvailableProjects = availableProjects;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditBuildServer(EditBuildServerViewModel model)
        {
            _monitorFacade.SaveBuildServer(model.Config);
            return RedirectToAction("EditBuildServer");
        }

        [HttpPost]
        public ActionResult GetProjects(EditBuildServerViewModel model)
        {
            var projects = _monitorFacade.GetAvailableProjectsFor(model.Config);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
    }
}