using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Ui.Syntax;

namespace DbToolGui.Highlighting
{
    public class DbToolSyntaxProvider : ISyntaxProvider
    {
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
                "new", "from", "in", "let", "select", "where", "orderby", "descending"});
        }

        private static IEnumerable<string> GetSettings()
        {
            var type = typeof (IDbToolSettings);
            return type.GetProperties().Select(property => property.Name.ToLowerInvariant());
        }

        public bool IsSetting(string word)
        {
            return _settings.Contains(word);
        }

        public bool IsSqlKeyword(string word)
        {
            return _sqlKeywords.Contains(word);
        }

        public bool IsCsharpKeyword(string word)
        {
            return _cSharpKeywords.Contains(word);
        }

        public bool IsFunction(string word)
        {
            return _functions.Contains(word);
        }

        public bool IsSeparator(char value)
        {
            return _separators.Contains(value);
        }

        public bool IsOperator(string word)
        {
            return _operators.Contains(word);
        }

        private bool IsObject(string word)
        {
            return _schemaObjectProvider.IsObject(word);
        }

        public TagType GetTypeOf(string word)
        {
            var lower = word.ToLowerInvariant();
            if (IsSqlKeyword(lower))
            {
                return TagType.Keyword;
            }
            if (IsCsharpKeyword(word))
            {
                return TagType.CSharp;
            }
            if (IsFunction(lower))
            {
                return TagType.Function;
            }
            if (IsOperator(lower))
            {
                return TagType.Operator;
            }
            if (IsObject(lower))
            {
                return TagType.Object;
            }
            if (IsSetting(lower))
            {
                return TagType.Setting;
            }
            return TagType.Nothing;
        }
    }
}