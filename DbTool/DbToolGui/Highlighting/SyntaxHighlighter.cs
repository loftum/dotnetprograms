using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;

namespace DbToolGui.Highlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        private const string StringPattern = @"'[^']*'";
        private readonly ISyntaxProvider _syntaxProvider;
        private readonly FlowDocument _document;
        private readonly IDictionary<TagType, HighlightStyle> _styles;

        public SyntaxHighlighter(FlowDocument document, ISyntaxProvider syntaxProvider)
        {
            _document = document;
            _syntaxProvider = syntaxProvider;
            _styles = new Dictionary<TagType, HighlightStyle>();
            _styles[TagType.Keyword] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
            _styles[TagType.Function] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DarkCyan));
            _styles[TagType.Operator] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Gray));
            _styles[TagType.String] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DarkRed));
            _styles[TagType.Default] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
            _styles[TagType.Object] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Olive));
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
            tags.AddRange(GetKeywordTagsIn(run));
            //tags.AddRange(GetStringTagsIn(run));
            Format(tags);
        }

        private IEnumerable<Tag> GetKeywordTagsIn(Run run)
        {
            var text = run.Text;
            var tags = new List<Tag>();

            var startIndex = 0;
            for (var ii = 0; ii < text.Length; ii++)
            {
                if (!_syntaxProvider.IsSeparator(text[ii]))
                {
                    continue;
                }
                if (ii > 0 && !(_syntaxProvider.IsSeparator(text[ii - 1])))
                {
                    var endIndex = ii;
                    var word = text.Substring(startIndex, endIndex - startIndex);

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
                                              run.ContentStart.GetPositionAtOffset(endIndex,
                                                                                   LogicalDirection.Backward)
                                      };
                        tags.Add(tag);
                    }
                }
                startIndex = ii + 1;
            }

            var lastWord = text.Substring(startIndex, text.Length - startIndex);
            var lastType = _syntaxProvider.GetTypeOf(lastWord);
            if (lastType != TagType.Nothing)
            {
                var tag = new Tag
                {
                    Type = lastType,
                    StartPosition =
                        run.ContentStart.GetPositionAtOffset(startIndex, LogicalDirection.Forward),
                    EndPosition =
                        run.ContentStart.GetPositionAtOffset(startIndex + lastWord.Length, LogicalDirection.Backward),
                    Word = lastWord
                };
                tags.Add(tag);
            }
            return tags;
        }

        private static IEnumerable<Tag> GetStringTagsIn(Run run)
        {
            var text = run.Text;
            var tags = new List<Tag>();
            var matches = new Regex(StringPattern).Matches(text);
            for (var ii=0; ii<matches.Count; ii++)
            {
                var group = matches[ii].Groups[0];
                var tag = new Tag
                    {
                        Type = TagType.String,
                        Word = group.Value,
                        StartPosition =
                            run.ContentStart.GetPositionAtOffset(group.Index,
                                                                LogicalDirection.Forward),
                        EndPosition =
                            run.ContentStart.GetPositionAtOffset(group.Index + group.Length,
                                                                LogicalDirection.Backward)
                    };
                tags.Add(tag);
            }
            return tags;
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