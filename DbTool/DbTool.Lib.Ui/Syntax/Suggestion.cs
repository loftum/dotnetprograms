namespace DbTool.Lib.Ui.Syntax
{
    public class Suggestion
    {
        public string Text { get; private set; }

        public Suggestion(string text)
        {
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}