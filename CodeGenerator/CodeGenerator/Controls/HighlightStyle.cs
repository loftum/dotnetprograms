using System.Windows.Media;
using CodeGenerator.Lib.Syntax;

namespace CodeGenerator.Controls
{
    public class HighlightStyle
    {
        public TagType Type { get; set; }
        public Brush Foreground { get; set; }

        public HighlightStyle()
        {
            Foreground = Brushes.Black;
        }
    }
}