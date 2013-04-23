using System;
using System.Web.Mvc;
using BasicManifest.Lib.Facades;

namespace BasicManifest.Web.Controllers
{
    public class CampController : BMControllerBase
    {
        private readonly ICampFacade _facade;

        public CampController(ICampFacade facade)
        {
            _facade = facade;
        }

        public ActionResult Index()
        {
            var model = _facade.GetCamps();
            return View(model);
        }

        public ActionResult Edit(long id)
        {
            var model = _facade.Edit(id);
            return View(model);
        }
    }
}