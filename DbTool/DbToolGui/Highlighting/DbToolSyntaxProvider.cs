using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;

namespace DbToolGui.Highlighting
{
    public class DbToolSyntaxProvider : ISyntaxProvider
    {
        public IEnumerable<string> Keywords { get; private set; }
        public IEnumerable<string> Functions { get; private set; }
        public IEnumerable<string> Operators { get; private set; }
        public IEnumerable<char> Separators { get; private set; }
        public IEnumerable<string> Settings { get; private set; }

        private readonly ISchemaObjectProvider _schemaObjectProvider;

        public DbToolSyntaxProvider(ISchemaObjectProvider schemaObjectProvider)
        {
            _schemaObjectProvider = schemaObjectProvider;
            Keywords = new[] { "select", "insert", "update", "delete", "drop", "distinct",
                "from", "left", "outer", "join", "on",
                "where", "and", "or", "not", "in",
                "group", "order", "by", "asc", "desc" };
            Functions = new[] {"migrate", "up", "down", "getschema", "set"};
            Operators = new[] {"+", "-", "*", "/", "=", "!=", "<", ">", "<>"};
            Separators = new[] {' ', '.','=', '+', '-', '<', '>'};
            Settings = GetSettings();
        }

        private static IEnumerable<string> GetSettings()
        {
            var type = typeof (IDbToolSettings);
            return type.GetProperties().Select(property => property.Name.ToLowerInvariant());
        }

        public bool IsSetting(string word)
        {
            var lower = word.ToLowerInvariant();
            return Settings.Any(s => s.Equals(lower));
        }

        public bool IsKeyword(string word)
        {
            var lower = word.ToLowerInvariant();
            return Keywords.Any(w => w.Equals(lower));
        }

        public bool IsFunction(string word)
        {
            var lower = word.ToLowerInvariant();
            return Functions.Any(w => w.Equals(lower));
        }

        public bool IsSeparator(char value)
        {
            return Separators.Any(s => s.Equals(value));
        }

        public bool IsOperator(string word)
        {
            var lower = word.ToLowerInvariant();
            return Operators.Any(o => o.Equals(lower));
        }

        private bool IsObject(string word)
        {
            return _schemaObjectProvider.IsObject(word);
        }

        public TagType GetTypeOf(string word)
        {
            if (IsKeyword(word))
            {
                return TagType.Keyword;
            }
            if (IsFunction(word))
            {
                return TagType.Function;
            }
            if (IsOperator(word))
            {
                return TagType.Operator;
            }
            if (IsObject(word))
            {
                return TagType.Object;
            }
            if (IsSetting(word))
            {
                return TagType.Setting;
            }
            return TagType.Nothing;
        }
    }
}