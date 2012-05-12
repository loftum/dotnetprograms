using System;
using System.Linq.Expressions;
using System.Web;
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

        public static IHtmlString IdFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property)
        {
            var fieldName = ExpressionHelper.GetExpressionText(property);
            var fieldId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(fieldName);
            return helper.Raw(fieldId);
        }

        public static IHtmlString JQueryIdFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property)
        {
            var id = helper.IdFor(property).ToString();
            return helper.Raw(string.Format("#{0}", id));
        }
    }
}