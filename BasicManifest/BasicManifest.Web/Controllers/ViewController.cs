using System.Web.Mvc;
using BasicManifest.Lib.Facades;

namespace BasicManifest.Web.Controllers
{
    public class ViewController : BMControllerBase
    {
        private readonly ICampFacade _campFacade;

        public ViewController(ICampFacade campFacade)
        {
            _campFacade = campFacade;
        }

        public ViewResult Index()
        {
            return View();
        }

        public PartialViewResult Skydivers()
        {
            return PartialView();
        }

        public PartialViewResult Camps()
        {
            return PartialView();
        }

        public ViewResult Camp(long id)
        {
            var model = _campFacade.Edit(id);
            return View(model);
        }
    }
}