using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace BuildMonitor.ExtensionMethods
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString TopMenuLink(this HtmlHelper helper, string linkText, string actionName, string controllerName, bool selected)
        {
            return helper.ActionLink(linkText, actionName, controllerName, null, new {@class = selected ? "activeTopMenu" : "inactiveTopMenu"});
        }
    }
}