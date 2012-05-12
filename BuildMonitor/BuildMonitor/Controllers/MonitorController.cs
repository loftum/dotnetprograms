using System;
using System.Web.Mvc;
using BuildMonitor.Lib.Api;
using BuildMonitor.Models.Monitor;

namespace BuildMonitor.Controllers
{
    public class MonitorController : BuildMonitorControllerBase
    {
        private readonly IMonitorFacade _monitorFacade;

        public MonitorController(IMonitorFacade monitorFacade)
        {
            _monitorFacade = monitorFacade;
        }

        public ActionResult Index()
        {
            var monitor = _monitorFacade.GetMonitor();
            if (!monitor.CanBeDisplayed)
            {
                return RedirectToAction("Index", "Admin");
            }
            var model = new MonitorIndexViewModel(monitor);
            return View(model);
        }

        public ActionResult GetLatestBuild(string id)
        {
            try
            {
                var result = _monitorFacade.GetLatestBuild(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}