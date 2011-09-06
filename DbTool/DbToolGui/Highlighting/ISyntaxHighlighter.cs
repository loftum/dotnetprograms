namespace DbToolGui.Highlighting
{
    public interface ISyntaxHighlighter
    {
        bool Running { get; }
        void StartHighlight();
        void StopHighlight();
        void Highlight();
        void DoHighlight();
    }
}