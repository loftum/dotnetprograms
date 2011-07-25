using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using StuffLibrary.Extensions;
using StuffLibrary.HtmlTools.Builders;

namespace StuffLibrary.HtmlTools
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString SubmitButton(this HtmlHelper htmlHelper, string title)
        {
            return HtmlSubmit.Create().WithValue(title).WithTitle(title).ToMvcHtmlString();
        }

        public static MvcHtmlString Button(this HtmlHelper htmlHelper, string id, string value, string title)
        {
            return HtmlButton.Create()
                .WithId(id)
                .WithValue(value)
                .WithTitle(title)
                .ToMvcHtmlString();
        }

        public static MvcHtmlString FlashMessages(this HtmlHelper htmlHelper)
        {
            var tempData = htmlHelper.ViewContext.Controller.TempData;
            if (tempData.HasFlashMessages())
            {
                var messages = tempData.GetFlashMessages();
                var builder = new StringBuilder();
                foreach (var flashMessage in messages)
                {
                    var div = HtmlDiv.Create().WithClass("flashMessage").WithContent(flashMessage.Message);
                    builder.Append(div.ToString());
                }
                return HtmlDiv.Create().WithClass("flashMessageContainer").ToMvcHtmlString();
            }
            return MvcHtmlString.Empty;
        }
    }
}