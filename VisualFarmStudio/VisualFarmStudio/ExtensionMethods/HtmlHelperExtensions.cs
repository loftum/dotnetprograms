using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace VisualFarmStudio.ExtensionMethods
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString JQueryIdFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> property)
        {
            var fieldName = ExpressionHelper.GetExpressionText(property);
            var fieldId = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(fieldName);
            var jQueryId = string.Format("#{0}", fieldId);
            return helper.Raw(jQueryId);
        }
    }
}