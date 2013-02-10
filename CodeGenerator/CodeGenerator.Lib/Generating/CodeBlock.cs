namespace CodeGenerator.Lib.Generating
{
    public class CodeBlock : TemplateElement
    {
        public const string Pattern = @"@\{{1}[\w\W]*\}{1}";
        private const string StartTagValue = "@{";
        private const string EndTagValue = "}";

        public TemplateElement StartTag { get; private set; }
        public TemplateElement Code { get; private set; }
        public TemplateElement EndTag { get; private set; }


        public CodeBlock(string rawText, int startIndex) : base(rawText, startIndex)
        {
            StartTag = new TemplateElement(rawText.Substring(0, StartTagValue.Length), startIndex);
            var endTagStartIndex = rawText.Length - EndTagValue.Length;
            EndTag = new TemplateElement(rawText.Substring(endTagStartIndex), endTagStartIndex);
            Code = new TemplateElement(rawText.Substring(StartTag.Length, rawText.Length - StartTag.Length - EndTag.Length), StartTag.Length);
            
        }
    }
}