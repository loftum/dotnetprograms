using System;
using System.Linq;
using System.Web.Mvc;
using BuildMonitor.Lib.Api;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Exceptions;
using BuildMonitor.Lib.UserInteraction;
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
            var config = _monitorFacade.GetConfig();
            var model = new EditSettingsViewModel(config);
            var availableProjects = _monitorFacade
                .GetAvailableProjectsFor(config.BuildServerConfig)
                .Select(project =>
                    new SelectListItem { Text = project.Name, Value = project.Id, Selected = config.BuildServerConfig.ProjectIds.Contains(project.Id) });
            model.BuildServer.AvailableProjects = availableProjects;
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveBuildServer(EditBuildServerViewModel model)
        {
            try
            {
                _monitorFacade.SaveBuildServer(model.Config);
                var message = UserMessage.Success("Success", "Build server saved");
                return Json(message, JsonRequestBehavior.AllowGet);
            }
            catch (FriendlyException ex)
            {
                var message = UserMessage.Error("Failure", ex.Message);
                return Json(message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult GetProjects(EditBuildServerViewModel model)
        {
            var projects = _monitorFacade.GetAvailableProjectsFor(model.Config);
            return Json(projects, JsonRequestBehavior.AllowGet);
        }
    }
}