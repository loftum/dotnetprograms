using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VisualFarmStudio.Lib.Facades;
using VisualFarmStudio.Lib.Interactive;
using VisualFarmStudio.Lib.Migrating;
using VisualFarmStudio.Lib.Model;
using VisualFarmStudio.Lib.UserSession;
using VisualFarmStudio.Models.Hack;

namespace VisualFarmStudio.Controllers
{
    public class HackController : VisualFarmControllerBase
    {
        private readonly IBondegardFacade _bondegardFacade;
        private readonly IInteractiveShell _interactiveShell;
        private readonly IVisualFarmMigrator _migrator;
        private readonly IUserManager _userManager;

        public HackController(IBondegardFacade bondegardFacade,
            IInteractiveShell interactiveShell,
            IVisualFarmMigrator migrator,
            IUserManager userManager)
        {
            _bondegardFacade = bondegardFacade;
            _interactiveShell = interactiveShell;
            _migrator = migrator;
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            var model = new HackIndexViewModel {MigrationVersion = _migrator.GetVersion()};
            return View(model);
        }

        public ActionResult GenerateData()
        {
            _bondegardFacade.GenerateBondegards(_userManager.CurrentUser.Bonde, 16);
            return RedirectToAction("Index");
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