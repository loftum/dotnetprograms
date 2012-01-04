using System.Collections.Generic;

namespace DbTool.Lib.CSharp
{
    public interface ICSharpEvaluator
    {
        IEnumerable<string> Vars { get; }
        IEnumerable<string> Usings { get; }
        void Init();
        void Using(params string[] nameSpaces);
        CSharpResult Run(string code);
    }
}