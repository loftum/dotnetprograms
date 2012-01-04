using System.Collections.Generic;
using DbTool.Lib.ExtensionMethods;
using Mono.CSharp;

namespace DbTool.Lib.CSharp
{
    public class MonoCSharpEvaluator : ICSharpEvaluator
    {
        public IEnumerable<string> Vars
        {
            get { return _evaluator.GetVars().SplitLines(); }
        }

        public IEnumerable<string> Usings
        {
            get { return _evaluator.GetUsing().SplitLines(); }
        } 

        private Evaluator _evaluator;

        public MonoCSharpEvaluator()
        {
            Init();
        }

        public void Init()
        {
            var report = new Report(new ConsoleReportPrinter());
            var parser = new CommandLineParser(report);
            var settings = parser.ParseArguments(new string[0]);
            settings.AssemblyReferences.Add("System.Core.dll");
            settings.AssemblyReferences.Add("WebMatrix.Data.dll");
            settings.AssemblyReferences.Add("DbTool.Lib.CSharp.dll");
            _evaluator = new Evaluator(settings, report);

            Using("System", "System.Linq", "System.Collections.Generic", "WebMatrix.Data", "DbTool.Lib.CSharp.WebMatrix");
            Run("var dummy = 1;");
            Run("var _query = new WebMatrixQuery();");
        }

        public void Using(params string[] nameSpaces)
        {
            nameSpaces.Each(nameSpace => _evaluator.Run(string.Format("using {0};", nameSpace)));
        }

        public CSharpResult Run(string code)
        {
            object result;
            bool resultSet;
            var message = _evaluator.Evaluate(code, out result, out resultSet);
            return new CSharpResult(resultSet, result, message);
        }
    }
}