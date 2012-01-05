using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Ui.Highlighting;
using DbTool.Lib.Ui.Syntax;
using DbToolGui.Views;

namespace DbToolGui.Highlighting
{
    public class SyntaxHighlighter : ISyntaxHighlighter
    {
        private Timer _timer;
        private readonly IDebugLogger _logger;

        private bool _highlighting;
        private readonly object _lock = new object();
        private bool _textChanged;
        private bool TextChanged
        {
            get
            {
                lock(_lock)
                {
                    return _textChanged;
                }
            }
            set
            {
                lock(_lock)
                {
                    _textChanged = value;
                }
            }
        }

        private readonly ISyntaxProvider _syntaxProvider;
        private readonly Dispatcher _dispatcher;
        private readonly RichTextBox _textBox;
        private readonly FlowDocument _document;
        private readonly IDictionary<TagType, HighlightStyle> _styles;

        public SyntaxHighlighter(RichTextBox textBox, Dispatcher dispatcher, ISyntaxProvider syntaxProvider)
        {
            _dispatcher = dispatcher;
            _textBox = textBox;
            _document = _textBox.Document;
            _syntaxProvider = syntaxProvider;
            _styles = new Dictionary<TagType, HighlightStyle>();
            _styles[TagType.CSharp] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DodgerBlue));
            _styles[TagType.SqlKeyword] = new HighlightStyle()
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
            _styles[TagType.Setting] = new HighlightStyle()
                .With(TextElement.ForegroundProperty, new SolidColorBrush(Colors.DarkMagenta));
            _textBox.TextChanged += HandleTextChanged;
            _logger = DebugLogger.Instance;
        }

        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged = true;
        }

        public void StartHighlight()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += HandleElapsed;
            _timer.Start();
        }

        private void HandleElapsed(object sender, ElapsedEventArgs e)
        {
            if (TextChanged && !_highlighting)
            {
                Highlight();
            }
        }

        public void StopHighlight()
        {
            _timer.Stop();
            _timer.Elapsed -= HandleElapsed;
            _timer.Dispose();
        }

        private void Highlight()
        {
            _highlighting = true;
            _dispatcher.Invoke(DispatcherPriority.Background, new Action(DoHighlight));
            TextChanged = false;
            _highlighting = false;
        }

        private void DoHighlight()
        {
            var textRange = new TextRange(_document.ContentStart, _document.ContentEnd);
            textRange.ClearAllProperties();

            var navigator = _document.ContentStart;
            var end = _document.ContentEnd;
            var tags = new List<Tag>();
            while (navigator.CompareTo(end) < 0)
            {
                var context = navigator.GetPointerContext(LogicalDirection.Backward);
                if (context == TextPointerContext.ElementStart && navigator.Parent is Run)
                {
                    var run = (Run) navigator.Parent;
                    tags.AddRange(GetKeywordTagsIn(run));
                }
                navigator = navigator.GetNextContextPosition(LogicalDirection.Forward);
            }
            Highlight(tags);
        }

        private void Highlight(IEnumerable<Tag> tags)
        {
            _textBox.TextChanged -= HandleTextChanged;
            Format(tags);
            _textBox.TextChanged += HandleTextChanged;
        }

        private IEnumerable<Tag> GetKeywordTagsIn(Run run)
        {
            var text = run.Text;
            var tags = new List<Tag>();
            if (text.IsSingleWord())
            {
                return tags;
            }

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
                                StartPosition = run.ContentStart.GetPositionAtOffset(startIndex, LogicalDirection.Forward),
                                EndPosition = run.ContentStart.GetPositionAtOffset(endIndex, LogicalDirection.Backward)
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

        private void Format(IEnumerable<Tag> tags)
        {
            foreach (var tag in tags)
            {
                var range = new TextRange(tag.StartPosition, tag.EndPosition);
                var style = GetStyleFor(tag.Type);
                if (style == null)
                {
                    continue;
                }
                foreach (var property in style.Properties)
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