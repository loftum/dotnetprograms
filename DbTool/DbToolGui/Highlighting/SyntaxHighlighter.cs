using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;

namespace DbToolGui.Highlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        private readonly ISyntaxProvider _syntaxProvider;
        private readonly FlowDocument _document;
        private readonly IDictionary<TagType, HighlightStyle> _styles;

        public SyntaxHighlighter(FlowDocument document)
        {
            _document = document;
            _syntaxProvider = new DbToolSyntaxProvider();
            _styles = new Dictionary<TagType, HighlightStyle>();
            _styles[TagType.Keyword] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
            _styles[TagType.Function] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DarkCyan));
            _styles[TagType.Operator] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Gray));
            _styles[TagType.Default] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
        }

        public void Highlight()
        {
            var textRange = new TextRange(_document.ContentStart, _document.ContentEnd);
            textRange.ClearAllProperties();

            var navigator = _document.ContentStart;
            while (navigator.CompareTo(_document.ContentEnd) < 0)
            {
                var context = navigator.GetPointerContext(LogicalDirection.Backward);
                if (context == TextPointerContext.ElementStart && navigator.Parent is Run)
                {
                    CheckWordsInRun((Run)navigator.Parent);
                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
        }

        private void CheckWordsInRun(Run run)
        {
            var tags = new List<Tag>();
            var text = run.Text;

            var startIndex = 0;
            int endIndex = 0;
            for (var ii = 0; ii < text.Length; ii++)
            {
                if (_syntaxProvider.IsSeparator(text[ii]))
                {
                    if (ii > 0 && !(_syntaxProvider.IsSeparator(text[ii-1])))
                    {
                        endIndex = ii - 1;
                        string word = text.Substring(startIndex, endIndex - startIndex + 1);

                        var type = _syntaxProvider.GetTypeOf(word);
                        if (type != TagType.Nothing)
                        {
                            var tag = new Tag
                                {
                                    Type = type,
                                    Word = word,
                                    StartPosition =
                                        run.ContentStart.GetPositionAtOffset(startIndex,
                                                                                LogicalDirection.Forward),
                                    EndPosition =
                                        run.ContentStart.GetPositionAtOffset(endIndex + 1,
                                                                                LogicalDirection.Backward)
                                };
                            tags.Add(tag);
                        }
                    }
                    startIndex = ii + 1;
                }
            }
            
            var lastWord = text.Substring(startIndex, text.Length - startIndex);
            if (_syntaxProvider.IsKeyword(lastWord))
            {
                var tag = new Tag
                    {
                        StartPosition =
                            run.ContentStart.GetPositionAtOffset(startIndex, LogicalDirection.Forward),
                        EndPosition =
                            run.ContentStart.GetPositionAtOffset(endIndex + 1, LogicalDirection.Backward),
                        Word = lastWord
                    };
                tags.Add(tag);
            }



            Format(tags);
        }

        private void Format(IEnumerable<Tag> tags)
        {
            foreach (var tag in tags)
            {
                var range = new TextRange(tag.StartPosition, tag.EndPosition);
                var highlight = GetStyleFor(tag.Type);
                if (highlight == null)
                {
                    continue;
                }
                foreach (var property in highlight.Properties)
                {
                    range.ApplyPropertyValue(property.Key, property.Value);
                }
            }
        }

        private HighlightStyle GetStyleFor(TagType type)
        {
            try
            {
                return _styles[type];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
}