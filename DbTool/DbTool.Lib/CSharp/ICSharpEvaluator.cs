using System.Collections.Generic;
using System.Reflection;
using DbTool.Lib.Syntax;

namespace DbTool.Lib.CSharp
{
    public interface ICSharpEvaluator
    {
        void Init();
        void ReferenceAssemblies(params Assembly[] assemblies);
        void Using(string nameSpace);
        CSharpResult Run(string code);
        IEnumerable<Suggestion> GetCompletions(string code);
    }
}