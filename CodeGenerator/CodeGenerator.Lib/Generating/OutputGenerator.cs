using System.Linq;
using System.Text;

namespace CodeGenerator.Lib.Generating
{
    public class OutputGenerator : IOutputGenerator
    {
        private readonly IInputParser _inputParser;
        private readonly ITemplateParser _templateParser;

        public OutputGenerator(IInputParser inputParser,
            ITemplateParser templateParser)
        {
            _inputParser = inputParser;
            _templateParser = templateParser;
        }

        public string Generate(string input, string template, int linesPerRecord, string delimiter)
        {
            var records = _inputParser.Parse(input, linesPerRecord, delimiter);
            var builder = new StringBuilder();
            
            foreach (var output in records.Select(record => _templateParser.Parse(template, record)))
            {
                builder.AppendLine(output);
            }
            return builder.ToString();
        }
    }
}