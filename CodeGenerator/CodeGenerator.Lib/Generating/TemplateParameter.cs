using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Generating
{
    public class TemplateParameter : TextElement
    {
        public const string Pattern = @"#[\d]+";

        public int Number { get; private set; }

        public TemplateParameter(string value, int startIndex) : base(value, startIndex)
        {
            Number = value.Substring(1).ConvertTo<int>();
        }
    }
}