using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu("Product", 0)]
    public class ProductController : MasterDataControllerBase
    {
        private readonly IProductFacade _productFacade;

        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        [MenuItem]
        public ActionResult List()
        {
            var products = _productFacade.GetProducts(null, 1, 20);
            return View(products);
        }
    }
}