using System.Web.Mvc;
using VisualFarmStudio.Lib.Facades;
using VisualFarmStudio.Lib.Interactive;
using VisualFarmStudio.Lib.Migrating;
using VisualFarmStudio.Lib.Model;
using VisualFarmStudio.Models.Hack;

namespace VisualFarmStudio.Controllers
{
    public class HackController : VisualFarmControllerBase
    {
        private readonly IBondegardFacade _bondegardFacade;
        private readonly IInteractiveShell _interactiveShell;
        private readonly IVisualFarmMigrator _migrator;

        public HackController(IBondegardFacade bondegardFacade,
            IInteractiveShell interactiveShell,
            IVisualFarmMigrator migrator)
        {
            _bondegardFacade = bondegardFacade;
            _interactiveShell = interactiveShell;
            _migrator = migrator;
        }

        public ActionResult Index()
        {
            var model = new HackIndexViewModel() {MigrationVersion = _migrator.GetVersion()};
            return View(model);
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

        [ValidateInput(false)]
        public JsonResult Execute(string statement)
        {
            var result = _interactiveShell.Execute(statement);
            return Json(result.Text, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MigrateUp()
        {
            _migrator.MigrateUp();
            return RedirectToAction("Index");
        }

        public ActionResult MigrateDown()
        {
            _migrator.MigrateDown();
            return RedirectToAction("Index");
        }
    }
}