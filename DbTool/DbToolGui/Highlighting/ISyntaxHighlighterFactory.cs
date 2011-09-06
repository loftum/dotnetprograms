using System.Windows.Controls;
using System.Windows.Threading;

namespace DbToolGui.Highlighting
{
    public interface ISyntaxHighlighterFactory
    {
        SyntaxHighlighter CreateFor(RichTextBox textBox, Dispatcher dispatcher);
    }
}