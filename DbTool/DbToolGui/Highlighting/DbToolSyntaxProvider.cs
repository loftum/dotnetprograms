using System.Collections.Generic;
using System.Linq;

namespace DbToolGui.Highlighting
{
    public class DbToolSyntaxProvider : ISyntaxProvider
    {
        public IEnumerable<string> Keywords { get; private set; }
        public IEnumerable<string> Functions { get; private set; }
        public IEnumerable<string> Operators { get; private set; }
        public IEnumerable<char> Separators { get; private set; }
        

        public DbToolSyntaxProvider()
        {
            Keywords = new[] {"select", "insert", "update", "delete", "drop", "distinct", "from", "left", "outer", "join", "on", "where", "in", "order", "by", "asc", "desc"};
            Functions = new[] {"migrate", "up", "down"};
            Operators = new[] {"+", "-", "*", "/", "="};
            Separators = new[] {' ', '.'};
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
            return TagType.Nothing;
        }
    }
}