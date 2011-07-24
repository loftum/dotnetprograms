namespace StuffLibrary.HtmlTools.Builders
{
    public class HtmlDiv : HtmlElementBuilder<HtmlDiv, HtmlDivElement>
    {
        private HtmlDiv(HtmlDivElement element) : base(element)
        {
        }

        public static HtmlDiv Create()
        {
            return new HtmlDiv(new HtmlDivElement());
        }
    }
}