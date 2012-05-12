using System.Web.Mvc;
using BuildMonitor.Lib.Api;
using BuildMonitor.Models.Monitor;

namespace BuildMonitor.Controllers
{
    public class MonitorController : BuildMonitorControllerBase
    {
        private readonly IBuildFacade _buildFacade;

        public MonitorController(IBuildFacade buildFacade)
        {
            _buildFacade = buildFacade;
        }

        public ActionResult Index()
        {
            var model = new MonitorIndexViewModel(_buildFacade.GetMonitor());
            return View(model);
        }
    }
}