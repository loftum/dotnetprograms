namespace VisualFarmStudio.Lib.Interactive
{
    public class CSharpResult
    {
        public string Text { get; private set; }

        public CSharpResult(string text)
        {
            Text = text;
        }
    }
}