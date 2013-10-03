using System;
using System.Web.Mvc;
using DotNetPrograms.Common.Exceptions;
using WebShop.Core.Facade;
using WebShop.Core.Users;

namespace WebShop.Web.Controllers
{
    public class CheckoutController : WebShopControllerBase
    {
        private readonly IWebShop _webShop;

        public CheckoutController(IWebShop webShop)
        {
            _webShop = webShop;
        }

        public ActionResult Start()
        {
            return RedirectIfBasketEmptyOr(() => RedirectToAction("Personalia"));
        }

        public ActionResult Personalia()
        {
            return RedirectIfBasketEmptyOr(() => View(_webShop.User.Basket.Personalia));
        }

        [HttpPost]
        public ActionResult Personalia(Personalia personalia)
        {
            try
            {
                personalia.Validate().OrThrowPropertyError();
                _webShop.User.Basket.Personalia = personalia;
                return RedirectToAction("Payment");
            }
            catch (PropertyErrorException ex)
            {
                AddError(ex);
                return View(personalia);
            }
        }

        public ActionResult Payment(Payment payment)
        {
            return RedirectIfBasketEmptyOr(() => View(_webShop.User.Basket.Payment));
        }

        private ActionResult RedirectIfBasketEmptyOr(Func<ActionResult> func)
        {
            var basket = _webShop.User.Basket;
            return basket.IsEmpty ? RedirectToReferrer() : func();
        }
    }
}