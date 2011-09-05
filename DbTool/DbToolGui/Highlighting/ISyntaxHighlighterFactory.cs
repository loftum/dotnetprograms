using System.Windows.Documents;

namespace DbToolGui.Highlighting
{
    public interface ISyntaxHighlighterFactory
    {
        SyntaxHighlighter CreateFor(FlowDocument document);
    }
}