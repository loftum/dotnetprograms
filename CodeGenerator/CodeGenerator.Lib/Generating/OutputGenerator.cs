using System.Linq;
using System.Text;

namespace CodeGenerator.Lib.Generating
{
    public class OutputGenerator : IOutputGenerator
    {
        private readonly IInputParser _inputParser;
        private readonly ITemplateEvaluator _templateEvaluator;

        public OutputGenerator(IInputParser inputParser,
            ITemplateEvaluator templateEvaluator)
        {
            _inputParser = inputParser;
            _templateEvaluator = templateEvaluator;
        }

        public string Generate(string input, string template, int linesPerRecord, string delimiter)
        {
            var records = _inputParser.Parse(input, linesPerRecord, delimiter);
            var builder = new StringBuilder();
            
            foreach (var output in records.Select(record => _templateEvaluator.Evaluate(template, record)))
            {
                builder.AppendLine(output);
            }
            return builder.ToString();
        }
    }
}