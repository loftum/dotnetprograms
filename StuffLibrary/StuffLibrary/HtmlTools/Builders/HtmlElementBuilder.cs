using System.Web.Mvc;

namespace StuffLibrary.HtmlTools.Builders
{
    public class HtmlElementBuilder<TBuilder, TElement>
        where TBuilder : HtmlElementBuilder<TBuilder, TElement>
        where TElement : HtmlElement
    {
        public TElement Element { get; private set; }

        protected TBuilder MySelf
        {
            get { return (TBuilder)this; }
        }

        public HtmlElementBuilder(TElement element)
        {
            Element = element;
        }

        public TBuilder WithId(string id)
        {
            Element.Id = id;
            return MySelf;
        }

        public TBuilder WithType(string type)
        {
            Element.Type = type;
            return MySelf;
        }

        public TBuilder WithName(string name)
        {
            Element.Name = name;
            return MySelf;
        }

        public TBuilder WithValue(string value)
        {
            Element.Value = value;
            return MySelf;
        }

        public TBuilder WithTitle(string title)
        {
            Element.Title = title;
            return MySelf;
        }

        public TBuilder WithOnClick(string onClick)
        {
            Element.OnClick = onClick;
            return MySelf;
        }

        public TBuilder WithClasses(params string[] cssClasses)
        {
            foreach (var cssClass in cssClasses)
            {
                WithClass(cssClass);
            }
            return MySelf;
        }

        public TBuilder WithClass(string cssClass)
        {
            Element.Classes.Add(cssClass);
            return MySelf;
        }

        public TBuilder WithEnabled(bool enabled)
        {
            return WithDisabled(!enabled);
        }

        public TBuilder WithDisabled(bool disabled)
        {
            Element.Disabled = disabled;
            return MySelf;
        }

        public TBuilder WithHidden(bool hidden)
        {
            Element.Hidden = hidden;
            return MySelf;
        }

        public TBuilder WithContent(object content)
        {
            Element.Content = content;
            return MySelf;
        }

        public override string ToString()
        {
            return Element.ToString();
        }

        public MvcHtmlString ToMvcHtmlString()
        {
            return MvcHtmlString.Create(Element.ToString());
        }
    }
}