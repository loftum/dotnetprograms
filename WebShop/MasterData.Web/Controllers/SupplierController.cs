using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model.Suppliers;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu]
    public class SupplierController : EditController<EditSupplierModel>
    {
        private readonly ISupplierFacade _supplierFacade;

        public SupplierController(ISupplierFacade supplierFacade)
        {
            _supplierFacade = supplierFacade;
        }

        [MenuItem]
        public ActionResult New()
        {
            return RedirectToAction("Edit", new {id = Guid.Empty});
        }

        [MenuItem]
        public ActionResult List()
        {
            var suppliers = _supplierFacade.GetSuppliers(null, 1, 20);
            return View(suppliers);
        }

        public override ActionResult Edit(Guid id)
        {
            try
            {
                Model = _supplierFacade.Edit(id);
                return View(Model);
            }
            catch (Exception ex)
            {
                AddError(ex);
                return RedirectToReferrer();
            }
        }

        [HttpPost]
        public override ActionResult Edit(EditSupplierModel input)
        {
            try
            {
                Model.UpdateFrom(input);
                var saved = _supplierFacade.Save(Model);
                Model = null;
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