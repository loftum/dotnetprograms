using System.Web.Mvc;
using WebShop.Core.Facade;
using WebShop.Core.Model;

namespace WebShop.Web.Controllers
{
    public class ProductController : WebShopControllerBase
    {
        private readonly IProductFacade _productFacade;

        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        public ActionResult Index()
        {
            var products = _productFacade.GetProducts(null, 1, 20);
            return View(products);
        }

        [HttpPost]
        public ActionResult Index(SearchInput searchInput)
        {
            var products = _productFacade.GetProducts(searchInput.Query, 1, 20);
            return View(products);
        }
    }
}