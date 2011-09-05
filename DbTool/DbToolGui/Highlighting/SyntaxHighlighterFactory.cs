using System.Windows.Documents;

namespace DbToolGui.Highlighting
{
    public class SyntaxHighlighterFactory : ISyntaxHighlighterFactory
    {
        private readonly ISyntaxProvider _syntaxProvider;

        public SyntaxHighlighterFactory(ISyntaxProvider syntaxProvider)
        {
            _syntaxProvider = syntaxProvider;
        }

        public SyntaxHighlighter CreateFor(FlowDocument document)
        {
            return new SyntaxHighlighter(document, _syntaxProvider);
        }
    }
}