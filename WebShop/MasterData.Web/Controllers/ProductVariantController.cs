using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model.Products;

namespace MasterData.Web.Controllers
{
    public class ProductVariantController : EditController<EditProductVariantModel>
    {
        private readonly IProductFacade _productFacade;

        public ProductVariantController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public override ActionResult Edit(Guid id)
        {
            Model = _productFacade.EditVariant(id);
            return View(Model);
        }

        public ActionResult NewForMaster(Guid id)
        {
            Model = _productFacade.NewVariantForMaster(id);
            return View("Edit", Model);
        }

        [HttpPost]
        public override ActionResult Edit(EditProductVariantModel input)
        {
            try
            {
                Model.UpdateFrom(input);
                var saved = _productFacade.Save(Model);
                AddSaveMessage(saved);
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