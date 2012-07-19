using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DbTool.Lib.Communication.DbCommands.Dynamic;
using DbTool.Lib.ExtensionMethods;
using Mono.CSharp;

namespace DbTool.Lib.CSharp.Mono
{
    public class MonoCSharpEvaluator : ICSharpEvaluator
    {
        private static readonly string[] InitialAssemblies = new[] { "System.Core.dll"};
        private static readonly string[] InitialUsings = new[]
            {
                "System",
                "System.Linq",
                "System.Linq.Expressions",
                "System.Collections.Generic",
                typeof(DynamicSqlQuery).Namespace
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

        public MonoCSharpEvaluator()
        {
            _stringWriter = new StringWriter();
            Init();
        }

        public void Init()
        {
            var printer = new StreamReportPrinter(_stringWriter);
            var settings = new CompilerSettings();
            settings.AssemblyReferences.AddRange(InitialAssemblies);
            var context = new CompilerContext(settings, printer);
            _evaluator = new Evaluator(context)
                {
                    InteractiveBaseClass = typeof (DbToolInteractive),
                    DescribeTypeExpressions = true,
                };
            ReferenceAssemblies(typeof(DynamicSqlQuery).Assembly);

            Using(InitialUsings);
            DbToolInteractive.Evaluator = _evaluator;
            DbToolInteractive.Output = _stringWriter;
        }

        private void ReferenceAssemblies(params Assembly[] assemblies)
        {
            assemblies.Each(a => _evaluator.ReferenceAssembly(a));
        }

        private void Using(params string[] nameSpaces)
        {
            nameSpaces.Each(nameSpace => _evaluator.Run(string.Format("using {0};", nameSpace)));
        }

        public CSharpResult Run(string code)
        {
            object result;
            bool resultSet;
            var message = _evaluator.Evaluate(code, out result, out resultSet);
            var report = _stringWriter.ToString();
            _stringWriter.GetStringBuilder().Clear();
            return new CSharpResult(resultSet, result, message, report);
        }
    }
}