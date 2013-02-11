namespace CodeGenerator.Lib.Generating
{
    public class CodeBlock : TextElement
    {
        public const string Pattern = @"¤\{{1}[\w\W]*\}¤{1}";
        private const string StartTagValue = "¤{";
        private const string EndTagValue = "}¤";

        public TextElement StartTag { get; private set; }
        public TextElement Code { get; private set; }
        public TextElement EndTag { get; private set; }


        public CodeBlock(string value, int startIndex) : base(value, startIndex)
        {
            StartTag = new TextElement(StartTagValue, startIndex);
            var endTagStartIndex = Bias(value.Length - EndTagValue.Length);
            EndTag = new TextElement(EndTagValue, endTagStartIndex);
            Code = new TextElement(value.Substring(StartTag.Length, value.Length - StartTag.Length - EndTag.Length), Bias(StartTag.Length));
        }

        public bool SpansIndex(int cursor)
        {
            return StartIndex <= cursor && cursor < EndIndex;
        }
    }
}