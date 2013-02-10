using System.Collections.Generic;

using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Syntax
{
    public class SyntaxProvider : ISyntaxProvider
    {
        private readonly IDictionary<string, TagType> _words;
        private readonly ISet<char> _separators;

        public SyntaxProvider()
        {
            _separators =
                new HashSet<char>(new[] {' ', '.', ',', ';', '=', '+', '-', '<', '>', '(', ')', '\t', '\n', '\r'});
            var cSharpKeywords = new HashSet<string>(new[]
                {
                    "var", "void", "string", "object", "dynamic",
                    "const", "int", "long", "double", "float", "decimal", "bool", "string", "true", "false", "char",
                    "public", "protected", "private", "virtual", "override", "static", "class",
                    "switch", "case", "default", "new", "in", "let", "orderby", "descending", "using"
                });
            var operators = new HashSet<string>(new[] { "+", "-", "*", "/", "=", "!", "<", ">", "<>" });

            _words = new Dictionary<string, TagType>();
            cSharpKeywords.Each(c => _words[c] = TagType.CSharp);
            operators.Each(o => _words[o] = TagType.Operator);
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
            if (_words.ContainsKey(word))
            {
                tagType = _words[word];
                return true;
            }
            tagType = TagType.Nothing;
            return false;
        }

        
        public TagType GetTypeOf(string word)
        {
            TagType tagType;
            return TryGetCaseSensitive(word, out tagType)
                ? tagType
                : TagType.Nothing;
        }
    }
}