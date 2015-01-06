using System.Collections.Generic;
using MongoTool.Core.Syntax;

namespace MongoTool.Core.CSharp
{
    public interface ICSharpEvaluator
    {
        CSharpResult Execute(string code);
        IEnumerable<Suggestion> GetCompletions(string code);
    }
}