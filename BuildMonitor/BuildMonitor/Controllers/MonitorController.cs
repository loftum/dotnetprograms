using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Api;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Model;
using BuildMonitor.Models.Monitor;

namespace BuildMonitor.Controllers
{
    public class MonitorController : BuildMonitorControllerBase
    {
        private readonly IBuildFacade _buildFacade;
        private readonly IBuildMonitorSettings _settings;

        public MonitorController(IBuildFacade buildFacade,
            IBuildMonitorSettings settings)
        {
            _buildFacade = buildFacade;
            _settings = settings;
        }

        public ActionResult Index()
        {
            var model = new MonitorIndexViewModel(_buildFacade.GetMonitor(), new MonitorInfo(_settings.BuildHost));
            return View(model);
        }

        public ActionResult GetLatestBuild(string id)
        {
            try
            {
                var result = _buildFacade.GetLatestBuild(id);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}