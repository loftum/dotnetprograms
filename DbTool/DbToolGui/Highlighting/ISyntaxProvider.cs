using System.Collections.Generic;

namespace DbToolGui.Highlighting
{
    public interface ISyntaxProvider
    {
        IEnumerable<string> Keywords { get; }
        IEnumerable<string> Operators { get; }
        bool IsKeyword(string word);
        bool IsFunction(string word);
        TagType GetTypeOf(string word);
        bool IsSeparator(char value);
    }
}