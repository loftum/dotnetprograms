using System.Collections.Generic;
using System.Text.RegularExpressions;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Ui.Highlighting;
using DbTool.Lib.Ui.Syntax;
using DbToolGui.Views;

namespace DbToolGui.Highlighting
{
    public class DbToolSyntaxParser : ISyntaxParser
    {
        private readonly IDebugLogger _logger;
        private readonly List<Tag> _tags;
        public IEnumerable<Tag> Tags { get { return _tags.AsReadOnly(); } }
        private readonly ISyntaxProvider _syntaxProvider;

        public DbToolSyntaxParser(ISyntaxProvider syntaxProvider)
        {
            _tags = new List<Tag>();
            _syntaxProvider = syntaxProvider;
            _logger = DebugLogger.Instance;
        }

        public void Parse(string text, int start, int end)
        {
            _tags.Clear();
            if (text.IsNullOrEmpty() || end <= start)
            {
                return;
            }

            var wordStart = 0;
            for (var ii = start; ii < end; ii++)
            {
                if (!_syntaxProvider.IsSeparator(text[ii]))
                {
                    continue;
                }
                if (ii > 0 && !(_syntaxProvider.IsSeparator(text[ii - 1])))
                {
                    var wordEnd = ii;
                    var word = text.Substring(wordStart, wordEnd - wordStart);

                    var type = _syntaxProvider.GetTypeOf(word);
                    if (type != TagType.Nothing)
                    {
                        var tag = new Tag
                        {
                            Type = type,
                            Word = word,
                            StartPosition = wordStart,
                            EndPosition = wordEnd
                        };
                        _tags.Add(tag);
                    }
                }
                wordStart = ii + 1;
            }

            _tags.AddRange(GetStringsIn(text));
        }

        private static IEnumerable<Tag> GetStringsIn(string text)
        {
            var strings = new List<Tag>();
            strings.AddRange(MatchesOf(text, "\"{1}[^\"]*\"{1}"));
            strings.AddRange(MatchesOf(text, @"'{1}[^']*'{1}"));
            return strings;
        }

        private static IEnumerable<Tag> MatchesOf(string text, string pattern)
        {
            var matches = Regex.Matches(text, pattern);
            var strings = new List<Tag>();
            for (var ii = 0; ii < matches.Count; ii++)
            {
                var group = matches[ii].Groups[0];
                strings.Add(new Tag { StartPosition = group.Index, EndPosition = group.Index + group.Length, Type = TagType.String, Word = group.Value });
            }
            return strings;
        }
    }
}