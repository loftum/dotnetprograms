using System.Collections.Generic;
using System.IO;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using Mono.CSharp;
using System;

namespace CodeGenerator.Lib.CSharp
{
    public class CSharpEvaluator : ICSharpEvaluator
    {
        private static readonly string[] InitialAssemblies = new[] { "System.Core.dll"};
        private static readonly string[] InitialUsings = new[]
            {
                "System",
                "System.Linq",
                "System.Linq.Expressions",
                "System.Collections.Generic"
            };

        public IEnumerable<string> Vars
        {
            get { return _evaluator.GetVars().SplitLines(); }
        }

        public IEnumerable<string> Usings
        {
            get { return _evaluator.GetUsing().SplitLines(); }
        }

        private Evaluator _evaluator;
        private readonly StringWriter _stringWriter;

        public CSharpEvaluator()
        {
            _stringWriter = new StringWriter();
            Init();
        }

        public void Init()
        {
            var printer = new StreamReportPrinter(_stringWriter);
            var settings = new CompilerSettings();
            settings.AssemblyReferences.AddRange(InitialAssemblies);
            var report = new Report(printer);
            _evaluator = new Evaluator(settings, report)
                {
                    InteractiveBaseClass = typeof (CodeGeneratorInteractive),
                    DescribeTypeExpressions = true,
                };

            TryUsing(InitialUsings);
            CodeGeneratorInteractive.Evaluator = _evaluator;
            CodeGeneratorInteractive.Output = _stringWriter;
        }

        private void TryUsing(params string[] nameSpaces)
        {
			try
			{
				nameSpaces.Each(nameSpace => _evaluator.Run(string.Format("using {0};", nameSpace)));
			}
			catch (NotSupportedException)
			{
				// Mono's Reflection.Emit does not currently support this for some reason.
			}
        }

        public IEnumerable<Suggestion> GetCompletions(string code)
        {
            string prefix;
            var completions = _evaluator.GetCompletions(code, out prefix);
            return completions == null
                ? Enumerable.Empty<Suggestion>()
                : completions.Select(c => new Suggestion(prefix, c));
        }

        public CSharpResult Run(string code)
        {
            object result;
            bool resultSet;
            var message = _evaluator.Evaluate(Terminate(code), out result, out resultSet);
            var report = _stringWriter.ToString();
            _stringWriter.GetStringBuilder().Clear();
            return new CSharpResult(resultSet, result, message, report);
        }

        private static string Terminate(string code)
        {
            return code.EndsWith(";")
                       ? code
                       : string.Format("{0};", code.Trim());
        }
    }
}