using System;
using System.Web.Mvc;
using WebShop.Core.Facade;
using WebShop.Core.Model;

namespace WebShop.Web.Controllers
{
    public class ProductController : WebShopControllerBase
    {
        private readonly IProductFacade _productFacade;
        private readonly IWebShop _webShop;

        public ProductController(IProductFacade productFacade,
            IWebShop webShop)
        {
            _productFacade = productFacade;
            _webShop = webShop;
        }

        public ActionResult Index()
        {
            var products = _productFacade.GetProducts(null, 1, 20);
            return View("List", products);
        }

        public ActionResult List()
        {
            var products = _productFacade.GetProducts(null, 1, 20);
            return View(products);
        }

        [HttpPost]
        public ActionResult List(SearchInput searchInput)
        {
            var products = _productFacade.GetProducts(searchInput.Query, 1, 20);
            return View(products);
        }

        public ActionResult View(Guid id)
        {
            var product = _productFacade.GetProduct(id);
            return View(product);
        }
    }
}