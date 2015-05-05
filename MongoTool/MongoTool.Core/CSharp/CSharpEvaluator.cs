using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using MongoTool.Core.Configuration;
using MongoTool.Core.Syntax;
using Mono.CSharp;

namespace MongoTool.Core.CSharp
{
    public class CSharpEvaluator : ICSharpEvaluator
    {
        private Evaluator _evaluator;
        private readonly StringBuilder _builder;
        private static readonly InteractiveSection InteractiveSection;

        static CSharpEvaluator()
        {
            var type = typeof (Enumerable);
            InteractiveSection = (InteractiveSection)ConfigurationManager.GetSection("interactive");
            new AssemblyLoader().Load(InteractiveSection.Assemblies);
        }

        public CSharpEvaluator()
        {

            _builder = new StringBuilder();
            _evaluator = Initialize(_builder);
        }

        public void Reset()
        {
            _evaluator = Initialize(_builder);
        }

        private static Evaluator Initialize(StringBuilder builder)
        {
            var settings = new CompilerSettings();
            var writer = new StringWriter(builder);
            var printer = new StreamReportPrinter(writer);
            var context = new CompilerContext(settings, printer);
            var evaluator = new Evaluator(context)
            {
                InteractiveBaseClass = typeof(Interactive),
                DescribeTypeExpressions = true
            };

            foreach (var assembly in AssemblyLoader.PreviouslyLoadedAssemblies)
            {
                Logger.Log(string.Format("skipped {0}", assembly.FullName));
            }
            Logger.Log(string.Format("==="));

            evaluator.ReferenceAssembly(typeof(CSharpEvaluator).Assembly);
            foreach (var assembly in AssemblyLoader.NewAssemblies)
            {
                Logger.Log(string.Format("Reference {0}", assembly.FullName));
                evaluator.ReferenceAssembly(assembly);
            }
            Logger.Log(string.Format("==="));

            var nameSpaces = new[]
            {
                "System",
                "System.Linq",
                "System.Linq.Expressions",
                "System.Collections.Generic"
            }.Concat(InteractiveSection.Namespaces.Select(n => n.Namespace));
            
            foreach (var nameSpace in nameSpaces)
            {
                Logger.Log(string.Format("using {0};", nameSpace));
                evaluator.Run(string.Format("using {0};", nameSpace));
            }
            return evaluator;
        }

        public CSharpResult Execute(string code)
        {
            try
            {
                bool resultSet;
                object result;
                var output = _evaluator.Evaluate(code, out result, out resultSet);
                var report = _builder.ToString();
                return new CSharpResult(output, result, resultSet, report);
            }
            catch (Exception ex)
            {
                return new CSharpResult(ex.ToString(), null, false, null);
            }
            finally
            {
                _builder.Clear();
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
    }
}