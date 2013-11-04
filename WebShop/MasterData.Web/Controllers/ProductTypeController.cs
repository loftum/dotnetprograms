using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu("Product type")]
    public class ProductTypeController : EditController<ProductTypeModel>
    {
        private readonly IProductTypeFacade _productTypeFacade;

        public ProductTypeController(IProductTypeFacade productTypeFacade)
        {
            _productTypeFacade = productTypeFacade;
        }

        [MenuItem]
        public ActionResult New()
        {
            return RedirectToAction("Edit", new {id = Guid.Empty});
        }

        [MenuItem]
        public ActionResult List()
        {
            var types = _productTypeFacade.GetProductTypes(null, 1, 20);
            return View(types);
        }

        public override ActionResult Edit(Guid id)
        {
            Model = _productTypeFacade.Edit(id);
            return View(Model);
        }

        [HttpPost]
        public override ActionResult Edit(ProductTypeModel input)
        {
            try
            {
                Model.UpdateFrom(input);
                var o = _productTypeFacade.Save(Model);
                AddSaveMessage(o);
                return RedirectToReferrer();
            }
            catch (Exception ex)
            {
                AddError(ex);
                return View(Model);
            }
        }
    }
}