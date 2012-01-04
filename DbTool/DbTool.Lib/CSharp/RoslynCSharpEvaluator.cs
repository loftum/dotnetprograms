using System.Collections.Generic;
using DbTool.Lib.ExtensionMethods;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;

namespace DbTool.Lib.CSharp
{
    public class RoslynCSharpEvaluator : ICSharpEvaluator
    {
        private Session _session;
        private ScriptEngine _engine;

        public RoslynCSharpEvaluator()
        {
            Init();
        }

        public IEnumerable<string> Vars
        {
            get { return "Roslyn doesn't know".AsArray(); }
        }

        public IEnumerable<string> Usings
        {
            get { return _engine.ImportedNamespaces.ToList(); }
        }

        public void Init()
        {
            _session = Session.Create();
            _engine = new ScriptEngine(new[]{"System.Core.dll", @".\DbTool.Lib.CSharp.dll", 
                @"c:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\WebMatrix.Data.dll"},
                new[] { "System", "System.Linq", "System.Collections.Generic", "WebMatrix.Data", "DbTool.Lib.CSharp.WebMatrix" });
            Run("var _query = new WebMatrixQuery();");
        }

        public void Using(params string[] nameSpaces)
        {
            nameSpaces.Each(n => _session.ImportNamespace(n));
        }

        public CSharpResult Run(string code)
        {
            var result = _engine.Execute(code, _session);
            var resultSet = result != null;
            return new CSharpResult(resultSet, result, string.Empty);
        }
    }
}