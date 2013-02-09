using System.Collections.Generic;

namespace CodeGenerator.Lib.CSharp
{
    public interface ICSharpEvaluator
    {
        void Init();
        CSharpResult Run(string code);
        IEnumerable<Suggestion> GetCompletions(string code);
    }
}