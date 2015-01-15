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
        public ActionResult Personalia(PersonaliaModel input)
        {
            try
            {
                input.Validate().OrThrowPropertyError();
                _webShop.User.Basket.Personalia = input;
                return RedirectToAction("Payment");
            }
            catch (PropertyErrorException ex)
            {
                AddError(ex);
                return View(input);
            }
        }

        public ActionResult Payment()
        {
            return RedirectIfBasketEmptyOr(() => View(_webShop.User.Basket.Payment));
        }

        [HttpPost]
        public ActionResult Payment(PaymentModel input)
        {
            try
            {
                input.Validate().OrThrowPropertyError();
                _webShop.User.Basket.Payment.UpdateFrom(input);
                return RedirectToAction("Confirm");
            }
            catch (PropertyErrorException ex)
            {
                AddError(ex);
                return View(input);
            }
        }

        public ActionResult Confirm()
        {
            return View(_webShop.User.Basket);
        }

        [HttpPost]
        public ActionResult Purchase()
        {
            try
            {
                var receipt = _webShop.FulfilPurchase();
                return RedirectToAction("Receipt");
            }
            catch (UserException ex)
            {
                AddError(ex);
                return RedirectToAction("Confirm");
            }
        }

        public ActionResult Receipt()
        {
            return View();
        }

        private ActionResult RedirectIfBasketEmptyOr(Func<ActionResult> func)
        {
            var basket = _webShop.User.Basket;
            return basket.IsEmpty ? RedirectToReferrer() : func();
        }
    }
}