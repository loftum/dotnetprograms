using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model.Products;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu("Product")]
    public class ProductMasterController : EditController<EditProductMasterModel>
    {
        private readonly IProductFacade _productFacade;

        public ProductMasterController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        [MenuItem("New Master")]
        public ActionResult New()
        {
            return RedirectToAction("Edit", new {id = Guid.Empty});
        }

        public override ActionResult Edit(Guid id)
        {
            try
            {
                Model = _productFacade.EditMaster(id);
                return View(Model);
            }
            catch (Exception ex)
            {
                AddError(ex);
                return RedirectToReferrer();
            }
        }

        [HttpPost]
        public override ActionResult Edit(EditProductMasterModel input)
        {
            try
            {
                Model.UpdateFrom(input);
                var saved = _productFacade.Save(Model);
                AddSaveMessage(saved);
                Model = null;
                return RedirectToAction("List", "Product");
            }
            catch (Exception ex)
            {
                AddError(ex);
                return View(Model);
            }
        }
    }
}