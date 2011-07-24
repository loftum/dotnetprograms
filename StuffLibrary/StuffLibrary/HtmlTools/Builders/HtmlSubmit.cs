namespace StuffLibrary.HtmlTools.Builders
{
    public class HtmlSubmit : HtmlInput<HtmlSubmit>
    {
        public HtmlSubmit()
            : base("submit")
        {
        }

        public static HtmlSubmit Create()
        {
            return new HtmlSubmit();
        }

        public static HtmlSubmit SaveButton()
        {
            return Create().WithId("saveButton").WithClasses("button", "saveButton");
        }

        public static HtmlSubmit CreateButton()
        {
            return Create().WithId("createButton").WithClasses("button", "createButton");
        }
    }
}