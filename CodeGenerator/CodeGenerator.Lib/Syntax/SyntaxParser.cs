using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Lib.CSharp;
using CodeGenerator.Lib.Generating;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Syntax
{
    public class SyntaxParser : ISyntaxParser
    {
        private readonly List<Tag> _tags;
        public IEnumerable<Tag> Tags { get { return _tags.AsReadOnly(); } }

        private readonly List<Suggestion> _suggestions;
        public IEnumerable<Suggestion> Suggestions
        {
            get { return _suggestions.AsReadOnly(); }
        }

        private readonly ISyntaxProvider _syntaxProvider;
        private readonly ICSharpEvaluator _cSharpEvaluator;
        

        public SyntaxParser(ISyntaxProvider syntaxProvider, ICSharpEvaluator cSharpEvaluator)
        {
            _tags = new List<Tag>();
            _suggestions = new List<Suggestion>();
            _syntaxProvider = syntaxProvider;
            _cSharpEvaluator = cSharpEvaluator;
        }

        public void FindSuggestions(string text, int cursor)
        {
            if (text.IsNullOrEmpty())
            {
                return;
            }
            _suggestions.Clear();
            _suggestions.AddRange(GetCompletions(text));
        }

        private IEnumerable<Suggestion> GetCompletions(string text)
        {
            return _cSharpEvaluator.GetCompletions(text).OrderBy(c => c.Text);
        }

        public void Parse(string text, int start, int end)
        {
            _tags.Clear();
            if (text.IsNullOrEmpty() || end <= start)
            {
                return;
            }
            _tags.AddRange(GetCodeBlockTagsIn(text));

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
                            StartPosition = wordStart,
                            EndPosition = wordEnd
                        };
                        _tags.Add(tag);
                    }
                }
                wordStart = ii + 1;
            }

            _tags.AddRange(GetStringsTagsIn(text));
        }

        private IEnumerable<Tag> GetCodeBlockTagsIn(string text)
        {
            var blocks = new List<Tag>();
            foreach (var block in GetCodeBlocksIn(text))
            {
                blocks.Add(new Tag{Type = TagType.CodeBlock, StartPosition = block.StartTag.StartIndex, EndPosition = block.StartTag.EndIndex });
                blocks.Add(new Tag{Type = TagType.CodeBlock, StartPosition = block.EndTag.StartIndex, EndPosition = block.EndTag.EndIndex});
            }
            return blocks;
        }

        public IEnumerable<Parameter> GetParametersIn(string text)
        {
            var regex = new Regex(Parameter.Pattern);
            var match = regex.Match(text);
            while (match.Success)
            {
                var group = match.Groups[0];
                yield return new Parameter(group.Value, group.Index);
                match = match.NextMatch();
            }
        }

        public IEnumerable<CodeBlock> GetCodeBlocksIn(string text)
        {
            var regex = new Regex(CodeBlock.Pattern);
            var match = regex.Match(text);
            while (match.Success)
            {
                var group = match.Groups[0];
                var codeBlock = new CodeBlock(group.Value, group.Index);
                yield return codeBlock;
                match = match.NextMatch();
            }
        }

        private static IEnumerable<Tag> GetStringsTagsIn(string text)
        {
            var strings = new List<Tag>();
            strings.AddRange(MatchesOf(text, "\"{1}[^\"]*\"{1}", TagType.String));
            return strings;
        }

        private static IEnumerable<Tag> MatchesOf(string text, string pattern, TagType tagType)
        {
            var matches = Regex.Matches(text, pattern);
            var strings = new List<Tag>();
            for (var ii = 0; ii < matches.Count; ii++)
            {
                var group = matches[ii].Groups[0];
                strings.Add(new Tag { StartPosition = group.Index, EndPosition = group.Index + group.Length, Type = tagType });
            }
            return strings;
        }
    }
}