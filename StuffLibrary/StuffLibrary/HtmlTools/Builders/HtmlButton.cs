namespace StuffLibrary.HtmlTools.Builders
{
    public class HtmlButton : HtmlInput<HtmlButton>
    {
        private HtmlButton() : base("button")
        {
        }

        public static HtmlButton Create()
        {
            return new HtmlButton();
        }
    }
}