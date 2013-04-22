namespace CodeGenerator.Lib.Generating
{
    public class TemplateParameter : TextElement
    {
        public const string Pattern = @"#[\d+\*]";

        public int? Number { get; private set; }
        public bool All { get { return !Number.HasValue; } }

        public TemplateParameter(string value, int startIndex) : base(value, startIndex)
        {
            int number;
            if (int.TryParse(value.Substring(1), out number))
            {
                Number = number;
            }
        }
    }
}