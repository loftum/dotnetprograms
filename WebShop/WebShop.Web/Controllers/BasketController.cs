using System;
using System.Web.Mvc;
using WebShop.Core.Facade;

namespace WebShop.Web.Controllers
{
    public class BasketController : WebShopControllerBase
    {
        private readonly IWebShop _webShop;

        public BasketController(IWebShop webShop)
        {
            _webShop = webShop;
        }

        public ActionResult Index()
        {
            return View("Show", _webShop.User.Basket);
        }

        public ActionResult Show()
        {
            return View(_webShop.User.Basket);
        }

        public ActionResult AddProduct(Guid id)
        {
            _webShop.AddToBasket(id);
            return RedirectToReferrer();
        }

        public ActionResult RemoveItem(int id)
        {
            _webShop.User.RemoveBasketItem(id);
            return RedirectToReferrer();
        }
    }
}