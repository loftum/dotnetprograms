using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model.Stores;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu("Store")]
    public class SalespointController : EditController<EditSalespointModel>
    {
        private readonly IStoreFacade _storeFacade;

        public SalespointController(IStoreFacade storeFacade)
        {
            _storeFacade = storeFacade;
        }

        [MenuItem("Salespoints")]
        public ActionResult List()
        {
            var salespoints = _storeFacade.SearchSalespoints(null, 1, 20);
            return View(salespoints);
        }

        public ActionResult NewForReseller(Guid id)
        {
            Model = _storeFacade.NewForReseller(id);
            return View("Edit", Model);
        }

        public override ActionResult Edit(Guid id)
        {
            Model = _storeFacade.EditSalespoint(id);
            return View(Model);
        }

        [HttpPost]
        public override ActionResult Edit(EditSalespointModel input)
        {
            try
            {
                Model.UpdateFrom(input);
                var saved = _storeFacade.Save(Model);
                AddSaveMessage(saved);
                return RedirectToAction("List", "Reseller");
            }
            catch (Exception ex)
            {
                AddError(ex);
                return View(Model);
            }
        }

        public ActionResult GenerateSaleProducts(Guid id)
        {
            try
            {
                _storeFacade.GenerateSaleProductsForSalespoint(id);
                AddSuccessMessage("SaleProducts generated", null);
            }
            catch (Exception ex)
            {
                AddError(ex);
            }
            return RedirectToAction("List");
        }
    }
}