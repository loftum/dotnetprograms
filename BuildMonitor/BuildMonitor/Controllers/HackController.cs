using System;
using System.Web.Mvc;
using BuildMonitor.Lib.Api;
using BuildMonitor.Models.Hack;

namespace BuildMonitor.Controllers
{
    public class HackController : BuildMonitorControllerBase
    {
        private readonly IMonitorFacade _monitorFacade;

        public HackController(IMonitorFacade monitorFacade)
        {
            _monitorFacade = monitorFacade;
        }

        public ActionResult Index()
        {
            return RedirectToAction("RestTest");
        }

        public ActionResult RestTest()
        {
            return View(new RestTestViewModel());
        }

        public ActionResult GetJson(string url)
        {
            try
            {
                var result = _monitorFacade.ReadJson(url);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}