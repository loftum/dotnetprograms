using System.Windows.Controls;
using System.Windows.Threading;
using DbTool.Lib.Ui.Syntax;

namespace DbToolGui.Highlighting
{
    public class SyntaxHighlighterFactory : ISyntaxHighlighterFactory
    {
        private readonly ISyntaxProvider _syntaxProvider;

        public SyntaxHighlighterFactory(ISyntaxProvider syntaxProvider)
        {
            _syntaxProvider = syntaxProvider;
        }

        public SyntaxHighlighter CreateFor(RichTextBox textBox, Dispatcher dispatcher)
        {
            return new SyntaxHighlighter(textBox, dispatcher, _syntaxProvider);
        }
    }
}