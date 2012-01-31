using System.Collections.Generic;
using DbTool.Lib.Ui.Syntax;

namespace DbTool.Lib.Ui.Highlighting
{
    public interface ISyntaxParser
    {
        IEnumerable<Tag> Tags { get; }
        void Parse(string text, int start, int end);
    }
}