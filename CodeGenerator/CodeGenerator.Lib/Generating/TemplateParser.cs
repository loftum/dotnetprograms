using System.Text;
using System.Text.RegularExpressions;
using CodeGenerator.Lib.CSharp;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Generating
{
    public class TemplateParser : ITemplateParser
    {
        private const string Parameter = @"#[\d]+";
        private const string Code = @"@\{{1}[\w\W]+\}{1}";

        private readonly ICSharpEvaluator _evaluator;

        public TemplateParser(ICSharpEvaluator evaluator)
        {
            _evaluator = evaluator;
        }

        public string Parse(string template, Record record)
        {
            var withRecordValues = ReplaceParameters(template, record);
            var withEvaluatedCode = EvaluateCode(withRecordValues);
            return withEvaluatedCode;
        }

        private string EvaluateCode(string withRecordValues)
        {
            var regex = new Regex(Code);
            var match = regex.Match(withRecordValues);
            var builder = new StringBuilder(withRecordValues);
            while(match.Success)
            {
                var group = match.Groups[0];
                var code = GetCode(group.Value);
                var codeResult = _evaluator.Run(code);
                var result = codeResult.ResultSet ? codeResult.Result : string.Empty;

                builder.Replace(group.Value, result.ToString());
                match = match.NextMatch();
            }
            return builder.ToString();
        }

        private static string GetCode(string input)
        {
            return input.Substring(2, input.Length - 3);
        }

        private string ReplaceParameters(string template, Record record)
        {
            var regex = new Regex(Parameter);
            var match = regex.Match(template);

            var builder = new StringBuilder(template);
            while (match.Success)
            {
                var group = match.Groups[0];
                var parameterNumber = group.Value.Replace("#", string.Empty).ConvertTo<int>();
                builder.Replace(group.Value, record[parameterNumber]);
                match = match.NextMatch();
            }
            return builder.ToString();
        }
    }
}