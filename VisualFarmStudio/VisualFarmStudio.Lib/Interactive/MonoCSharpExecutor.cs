using System.IO;
using System.Text;
using System.Web;
using Mono.CSharp;
using VisualFarmStudio.Common.Configuration;
using VisualFarmStudio.Common.ExtensionMethods;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.Facades;

namespace VisualFarmStudio.Lib.Interactive
{
    public class MonoCSharpExecutor : ICSharpExecutor
    {
        private readonly Evaluator _evaluator;
        private readonly StringBuilder _reportBuilder;

        public MonoCSharpExecutor()
        {
            _reportBuilder = new StringBuilder();
            var writer = new StringWriter(_reportBuilder);
            var printer = new StreamReportPrinter(writer);
            var settings = new CompilerSettings();
            
            var context = new CompilerContext(settings, printer);
            _evaluator = new Evaluator(context);
            _evaluator.InteractiveBaseClass = typeof (InteractiveStuff);
            _evaluator.ReferenceAssembly(typeof(HttpContext).Assembly);
            _evaluator.ReferenceAssembly(typeof(VisualFarmRepo).Assembly);
            _evaluator.ReferenceAssembly(typeof(BondegardFacade).Assembly);
            _evaluator.ReferenceAssembly(typeof(IVFSConfig).Assembly);
            Execute("using System;");
            Execute("using System.Linq;");
            Execute("using System.Web;");
            Execute("using VisualFarmStudio.Core.Domain;");
            Execute("using VisualFarmStudio.Core.Repository;");
            Execute("using VisualFarmStudio.Lib.Model;");
            Execute("using VisualFarmStudio.Lib.Containers;");
        }

        public string Vars { get { return _evaluator.GetVars(); } }

        public CSharpResult Execute(string statement)
        {
            object result;
            bool resultSet;
            _evaluator.Evaluate(statement, out result, out resultSet);

            var builder = new StringBuilder();
            if (resultSet)
            {
                builder.AppendLine(Prepare((dynamic)result));
            }
            builder.AppendLine(_reportBuilder.ToString());
            _reportBuilder.Clear();

            return new CSharpResult(builder.ToString());
        }

        private static string Prepare(string result)
        {
            return result;
        }

        private static string Prepare(object result)
        {
            return result.ToJson(true);
        }
    }
}