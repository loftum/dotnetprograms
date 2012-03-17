using System.Web.Mvc;
using VisualFarmStudio.Core.Domain;
using VisualFarmStudio.Lib.Facades;
using VisualFarmStudio.Lib.Interactive;
using VisualFarmStudio.Lib.Model;
using VisualFarmStudio.Models.Hack;

namespace VisualFarmStudio.Controllers
{
    public class HackController : VFSControllerBase
    {
        private readonly IBondegardFacade _bondegardFacade;
        private readonly ICSharpExecutor _cSharpExecutor;

        public HackController(IBondegardFacade bondegardFacade, ICSharpExecutor cSharpExecutor)
        {
            _bondegardFacade = bondegardFacade;
            _cSharpExecutor = cSharpExecutor;
        }

        public ActionResult Index()
        {
            return View(new HackViewModel());
        }

        public ActionResult GenerateData()
        {
            for(var ii=0; ii<50; ii++)
            {
                _bondegardFacade.Save(CreateBondegard(ii));
            }
            return RedirectToAction("Index");
        }

        private BondegardModel CreateBondegard(int id)
        {
            var bondegard = new BondegardModel();
            bondegard.Navn = string.Format("Bondegård {0}", id);
            return bondegard;
        }

        public ActionResult Interactive()
        {
            return View(new InteractiveViewModel());
        }

        [HttpPost]
        public ActionResult Interactive(InteractiveViewModel model)
        {
            model.Result = _cSharpExecutor.Execute(model.Input);
            return View(model);
        }

        public JsonResult Execute(string statement)
        {
            var result = _cSharpExecutor.Execute(statement);
            return Json(result.Text, JsonRequestBehavior.AllowGet);
        }
    }
}