using System.Web.Mvc;
using BuildMonitor.ExtensionMethods;
using BuildMonitor.Models.Shared;

namespace BuildMonitor.Controllers
{
    public abstract class BuildMonitorControllerBase : Controller
    {
        protected BuildMonitorControllerBase()
        {
            var topModel = new TopModel
                {
                    CurrentMenu = GetType().Name.Replace("Controller", string.Empty)
                };
            ViewData.SetTopModel(topModel);
        }
    }
}