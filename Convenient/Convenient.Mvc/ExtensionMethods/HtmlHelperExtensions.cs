using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Convenient.ExtensionMethods;
using Convenient.Models;

namespace Convenient.Mvc.ExtensionMethods
{
    public static class HtmlHelperExtensions
    {
        public static string PropertyName<TModel, TProperty>(this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TProperty>> property)
        {
            return property.GetPropertyName();
        }

        public static MvcHtmlString Editor(this HtmlHelper helper, SimpleObjectModel model, object htmlAttributes = null)
        {
            if (model is EnumObjectModel)
            {
                return helper.DropDown((EnumObjectModel)model, htmlAttributes);
            }
            if (model.Type == typeof(bool))
            {
                return helper.CheckBox(model.FullName, (bool)model.Value, htmlAttributes);
            }
            var textBox = helper.TextBox(model.FullName, model.Value, htmlAttributes);
            return textBox;
        }

        public static MvcHtmlString DropDown(this HtmlHelper helper, EnumObjectModel model, object htmlAttributes = null)
        {
            var attributes = htmlAttributes == null
                ? ""
                : string.Join(" ", htmlAttributes.GetType().GetProperties().Select(p => string.Format("'{0}'='{1}'", p.Name, p.GetValue(htmlAttributes))));

            var values = model.EnumValues
                .Select(v => new SelectListItem { Text = v.ToString(), Value = v.ToString(), Selected = v.Equals(model.Value) })
                .ToList();

            var builder = new StringBuilder().AppendFormat("<select id='{0}' name='{1}' {2}>", model.FullName.Replace('.', '_'), model.FullName, attributes);
            foreach (var value in values)
            {
                builder.AppendFormat("<option value='{0}'", value.Value);
                if (value.Selected)
                {
                    builder.AppendFormat(" selected='selected'");
                }
                builder.Append(">").AppendFormat("{0}</option>", value.Text);
            }
            builder.Append("</select>");
            return new MvcHtmlString(builder.ToString());
        }
    }
}