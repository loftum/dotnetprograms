using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MasterData.Web.ExtensionMethods
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLinkWithId(this HtmlHelper helper, string linkText,
                                                             string action, string controller, Guid id)
        {
            return helper.ActionLink(linkText, action, controller, new {id}, null);
        }
    }
}