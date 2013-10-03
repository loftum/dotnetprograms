using System;
using System.Web.Mvc;
using DotNetPrograms.Common.UserInteraction;
using WebShop.Web.ExtensionMethods;

namespace WebShop.Web.Controllers
{
    public class WebShopControllerBase : Controller
    {
        protected ActionResult RedirectToReferrer()
        {
            return Redirect(GetReferrer());
        }

        protected string GetReferrer()
        {
            return (Request != null && Request.UrlReferrer != null && Request.Path != Request.UrlReferrer.PathAndQuery)
                ? Request.UrlReferrer.PathAndQuery
                : "~/";
        }

        protected void AddError(Exception ex)
        {
            var message = UserMessage.Error(ex.Message, "");
            TempData.AddMessage(message);
        }
    }
}