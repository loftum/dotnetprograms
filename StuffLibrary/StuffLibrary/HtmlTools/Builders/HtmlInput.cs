namespace StuffLibrary.HtmlTools.Builders
{
    public class HtmlInput<TBuilder> : HtmlElementBuilder<TBuilder, HtmlInputElement>
        where TBuilder : HtmlElementBuilder<TBuilder, HtmlInputElement>
    {
        public HtmlInput(string type)
            : base(new HtmlInputElement())
        {
            WithType(type);
        }
    }
}