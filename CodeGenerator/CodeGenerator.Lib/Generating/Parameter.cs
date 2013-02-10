using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Generating
{
    public class Parameter : TemplateElement
    {
        public const string Pattern = @"#[\d]+";

        public int Number { get; private set; }

        public Parameter(string rawText, int startIndex) : base(rawText, startIndex)
        {
            Number = rawText.Substring(1).ConvertTo<int>();
        }
    }
}