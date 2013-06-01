using System.Web.Mvc;
using BasicManifest.Lib.Facades;

namespace BasicManifest.Web.Controllers
{
    public class SkydiverController : BMControllerBase
    {
        private readonly ICampFacade _campFacade;

        public SkydiverController(ICampFacade campFacade)
        {
            _campFacade = campFacade;
        }

        public JsonResult ForCamp(long id)
        {
            var camp = _campFacade.Edit(id);
            return Json(camp.Skydivers, JsonRequestBehavior.AllowGet);
        }
    }
}