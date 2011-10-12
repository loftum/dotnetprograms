using System.Collections.Generic;

namespace DbTool.Lib.Ui.Syntax
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