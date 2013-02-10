using System.Collections.Generic;
using CodeGenerator.Lib.CSharp;
using CodeGenerator.Lib.Generating;

namespace CodeGenerator.Lib.Syntax
{
    public interface ISyntaxParser
    {
        IEnumerable<Tag> Tags { get; }
        void Parse(string text, int start, int end);
        IEnumerable<Suggestion> Suggestions { get; }
        void FindSuggestions(string text, int cursor);
        IEnumerable<CodeBlock> GetCodeBlocksIn(string text);
        IEnumerable<Parameter> GetParametersIn(string text);
    }
}