using System.Web.Mvc;
using WebShop.Core.Facade;

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
            return View(_productFacade.GetProducts(1, 20));
        }
    }
}