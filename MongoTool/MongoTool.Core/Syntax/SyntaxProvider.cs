using System.Collections.Generic;

namespace MongoTool.Core.Syntax
{
    public class SyntaxProvider : ISyntaxProvider
    {
        private readonly IDictionary<string, TagType> _caseSensitiveWords;
        private readonly IDictionary<string, TagType> _words;
        private readonly ISet<string> _cSharpKeywords;
        private readonly ISet<string> _functions;
        private readonly ISet<string> _operators;
        private readonly ISet<char> _separators;

        public SyntaxProvider()
        {
            _operators = new HashSet<string>(new[] { "+", "-", "*", "/", "=", "!", "<", ">", "<>" });
            _separators = new HashSet<char>(new[] { ' ', '.', ',', ';', '=', '+', '-', '<', '>', '(', ')', '\t', '\n', '\r' });
            
            _cSharpKeywords = new HashSet<string>(new[]{"var", "void", "string", "object", "dynamic",
                "const", "int", "long", "double", "float", "decimal",  "bool", "true", "false", "char",
                "public", "protected", "private", "virtual", "override", "static", "class",
                "switch", "case", "default", "new", "in", "let", "orderby", "descending", "using"});

            _caseSensitiveWords = new Dictionary<string, TagType>();
            foreach (var cSharpKeyword in _cSharpKeywords)
            {
                _caseSensitiveWords[cSharpKeyword] = TagType.CSharp;
            }

            _words = new Dictionary<string, TagType>();
            foreach (var o in _operators)
            {
                _words[o] = TagType.Operator;
            }
        }


        public bool IsSeparator(char value)
        {
            return _separators.Contains(value);
        }

        public bool IsPropertyIndicator(char c)
        {
            return c == '.';
        }

        private bool TryGetCaseSensitive(string word, out TagType tagType)
        {
            if (_caseSensitiveWords.ContainsKey(word))
            {
                tagType = _caseSensitiveWords[word];
                return true;
            }
            tagType = TagType.Nothing;
            return false;
        }

        private bool TryGetCaseInsensitive(string lowerCase, out TagType tagType)
        {
            if (_words.ContainsKey(lowerCase))
            {
                tagType = _words[lowerCase];
                return true;
            }
            tagType = TagType.Nothing;
            return false;
        }


        public TagType GetTypeOf(string word)
        {
            TagType tagType;
            if (TryGetCaseSensitive(word, out tagType))
            {
                return tagType;
            }
            var lower = word.ToLowerInvariant();
            if (TryGetCaseInsensitive(lower, out tagType))
            {
                return tagType;
            }
            return TagType.Nothing;
        }
    }
}