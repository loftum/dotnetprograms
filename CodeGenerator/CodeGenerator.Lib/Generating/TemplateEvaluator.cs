using System.Text;
using CodeGenerator.Lib.CSharp;
using CodeGenerator.Lib.Syntax;

namespace CodeGenerator.Lib.Generating
{
    public class TemplateEvaluator : ITemplateEvaluator
    {
        private readonly ICSharpEvaluator _evaluator;
        private readonly ISyntaxParser _syntaxParser;

        public TemplateEvaluator(ICSharpEvaluator evaluator, ISyntaxParser syntaxParser)
        {
            _evaluator = evaluator;
            _syntaxParser = syntaxParser;
        }

        public string Evaluate(string template, Record record)
        {
            var withRecordValues = ReplaceParameters(template, record);
            var withEvaluatedCode = EvaluateCode(withRecordValues);
            return withEvaluatedCode;
        }

        private string EvaluateCode(string withRecordValues)
        {
            var builder = new StringBuilder(withRecordValues);
            foreach (var codeBlock in _syntaxParser.GetCodeBlocksIn(withRecordValues))
            {
                var codeResult = _evaluator.Run(codeBlock.Code.RawText);
                var result = codeResult.ResultSet ? codeResult.Result : string.Empty;
                builder.Replace(codeBlock.RawText, result.ToString());
            }
            return builder.ToString();
        }

        private string ReplaceParameters(string template, Record record)
        {
            var builder = new StringBuilder(template);
            foreach (var parameter in _syntaxParser.GetParametersIn(template))
            {
                builder.Replace(parameter.RawText, record[parameter.Number]);
            }
            return builder.ToString();
        }
    }
}