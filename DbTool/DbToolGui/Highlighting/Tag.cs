using System.Windows.Documents;
using DbTool.Lib.Ui.Syntax;

namespace DbToolGui.Highlighting
{
    public struct Tag
    {
        public TagType Type { get; set; }
        public string Word { get; set; }
        public TextPointer StartPosition { get; set; }
        public TextPointer EndPosition { get; set; }
    }
}