using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model.Stores;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu("Store")]
    public class ResellerController : EditController<EditResellerModel>
    {
        private readonly IStoreFacade _storeFacade;

        public ResellerController(IStoreFacade storeFacade)
        {
            _storeFacade = storeFacade;
        }

        [MenuItem("New Reseller")]
        public ActionResult New()
        {
            return RedirectToAction("Edit", new {id = Guid.Empty});
        }

        [MenuItem("Resellers")]
        public ActionResult List()
        {
            var resellers = _storeFacade.SearchResellers(null, 1, 20);
            return View(resellers);
        }

        public override ActionResult Edit(Guid id)
        {
            Model = _storeFacade.EditReseller(id);
            return View(Model);
        }

        [HttpPost]
        public override ActionResult Edit(EditResellerModel input)
        {
            Model.UpdateFrom(input);
            try
            {
                var saved = _storeFacade.Save(Model);
                AddSaveMessage(saved);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                AddError(ex);
                return View(Model);
            }
        }
    }
}