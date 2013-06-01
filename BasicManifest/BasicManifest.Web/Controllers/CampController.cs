using System.Linq;
using System.Web.Mvc;
using BasicManifest.Lib.ExtensionMethods;
using BasicManifest.Lib.Facades;
using BasicManifest.Lib.Models;

namespace BasicManifest.Web.Controllers
{
    public class CampController : BMControllerBase
    {
        private readonly ICampFacade _facade;

        public CampController(ICampFacade facade)
        {
            _facade = facade;
        }

        public JsonResult List()
        {
            var camps = _facade.GetCamps().Camps.Select(c => c.MapTo<CampModel>());
            return Json(camps, JsonRequestBehavior.AllowGet);
        }
    }
}