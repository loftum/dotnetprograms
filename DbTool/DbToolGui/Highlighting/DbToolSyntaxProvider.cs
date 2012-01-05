using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Ui.Syntax;

namespace DbToolGui.Highlighting
{
    public class DbToolSyntaxProvider : ISyntaxProvider
    {
        private readonly IDictionary<string, TagType> _caseSensitiveWords;
        private readonly IDictionary<string, TagType> _words;
        private readonly ISet<string> _sqlKeywords;
        private readonly ISet<string> _cSharpKeywords;
        private readonly ISet<string> _functions;
        private readonly ISet<string> _operators;
        private readonly ISet<char> _separators;
        private readonly ISet<string> _settings;

        private readonly ISchemaObjectProvider _schemaObjectProvider;

        public DbToolSyntaxProvider(ISchemaObjectProvider schemaObjectProvider)
        {
            _schemaObjectProvider = schemaObjectProvider;
            _sqlKeywords = new HashSet<string>(new[]{ "select", "insert", "update", "delete", "drop", "distinct",
                "from", "left", "outer", "join", "on",
                "where", "and", "or", "not", "in",
                "group", "order", "by", "asc", "desc" });
            _functions = new HashSet<string>(new[] { "migrate", "up", "down", "show", "set", "vars", "usings", "$" });
            _operators = new HashSet<string>(new[] {"+", "-", "*", "/", "=", "!=", "<", ">", "<>"});
            _separators = new HashSet<char>(new[] {' ', '.','=', '+', '-', '<', '>', '(', ')', '\t', '\n', '\r'});
            _settings = new HashSet<string>(GetSettings());
            _cSharpKeywords = new HashSet<string>(new[]{"var", "void", "string", "object", "dynamic",
                "int", "long", "double", "float", "decimal",  "bool", "char",
                "new", "in", "let", "orderby", "descending"});
            
            _caseSensitiveWords = new Dictionary<string, TagType>();
            _cSharpKeywords.Each(c => _caseSensitiveWords[c] = TagType.CSharp);

            _words = new Dictionary<string, TagType>();
            _sqlKeywords.Each(w => _words[w] = TagType.SqlKeyword);
            _functions.Each(f => _words[f] = TagType.Function);
            _operators.Each(o => _words[o] = TagType.Operator);
            _settings.Each(s => _words[s] = TagType.Setting);
        }

        private static IEnumerable<string> GetSettings()
        {
            var type = typeof (IDbToolSettings);
            return type.GetProperties().Select(property => property.Name.ToLowerInvariant());
        }

        public bool IsSeparator(char value)
        {
            return _separators.Contains(value);
        }

        private bool IsObject(string word)
        {
            return _schemaObjectProvider.IsObject(word);
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
            if (IsObject(lower))
            {
                return TagType.Object;
            }
            return TagType.Nothing;
        }
    }
}