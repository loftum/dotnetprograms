using System.Collections.Generic;
using DbTool.Lib.Syntax;

namespace DbTool.Lib.CSharp
{
    public interface ICSharpEvaluator
    {
        void Init();
        CSharpResult Run(string code);
        IEnumerable<Suggestion> GetCompletions(string code);
    }
}