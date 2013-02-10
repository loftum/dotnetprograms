namespace CodeGenerator.Lib.Generating
{
    public class TemplateElement
    {
        public int StartIndex { get; private set; }
        public int Length { get; private set; }
        public int EndIndex { get { return StartIndex + Length; } }
        public string RawText { get; private set; }

        public TemplateElement(string rawText, int startIndex)
        {
            RawText = rawText;
            StartIndex = startIndex;
            Length = rawText.Length;
        }
    }
}