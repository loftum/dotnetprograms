using System;
using System.Web.Mvc;
using DotNetPrograms.Common.Exceptions;
using DotNetPrograms.Common.UserInteraction;
using MasterData.Core.Model;
using MasterData.Web.ExtensionMethods;

namespace MasterData.Web.Controllers
{
    public abstract class MasterDataControllerBase : Controller
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
            AddErrorMessageFor(ex);
        }

        protected void AddError(PropertyErrorException ex)
        {
            foreach (var error in ex.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }
            AddErrorMessageFor(ex);
        }

        protected void AddSaveMessage(IObjectIdentifier identifier)
        {
            AddSuccessMessage(string.Format("{0} was saved", identifier.Name), null);
        }

        protected void AddSuccessMessage(string title, string message)
        {
            TempData.AddMessage(UserMessage.Success(title, message));
        }

        private void AddErrorMessageFor(Exception ex)
        {
            var message = UserMessage.Error(ex.Message, ex.ToString());
            TempData.AddMessage(message);
        }

        protected ActionResult RedirectToHome()
        {
            return Redirect("~/");
        }
    }
}