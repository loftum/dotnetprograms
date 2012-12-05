using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbTool.Lib.Linq
{
    public class DbToolSql
    {
        public string Verb { get; set; }
        public string Statement { get { return GetStatement(); } }
        public int Count { get; set; }
        public string What { get; set; }
        public string Table { get; set; }
        private readonly IList<string> _wheres = new List<string>();
        public string Where { get { return string.Join(" and ", _wheres); } }

        public string CommandText { get { return BuildCommandText(); } }
        public IDictionary<string, DbToolSqlParameter> Parameters { get; set; }

        

        public DbToolSql()
        {
            Verb = "select";
            What = "*";
            Parameters = new Dictionary<string, DbToolSqlParameter>();
        }

        private string GetStatement()
        {
            var builder = new StringBuilder(Verb);
            if (Verb.Equals("select") && Count > 0)
            {
                builder.AppendFormat(" top {0}", Count);
            }
            builder.AppendFormat(" {0}", What);
            return builder.ToString();
        }

        private string BuildCommandText()
        {
            var builder = new StringBuilder(Statement)
                .AppendFormat(" from {0}", Table);
            if (_wheres.Any())
            {
                builder.AppendFormat(" where {0}", Where);
            }    
            return builder.ToString();
        }

        public void AppendWhere(string where)
        {
            _wheres.Add(where);
        }

        public override string ToString()
        {
            var builder = new StringBuilder(CommandText).AppendLine();
            var parameters = Parameters.Values.Select(p => string.Format("{0}='{1}'", p.Name, p.Value));
            if (parameters.Any())
            {
                builder.AppendFormat("[ {0} ]", string.Join(" ", parameters));
            }
            return builder.ToString();
        }

        public DbToolSqlParameter NewParameter()
        {
            var parameter = new DbToolSqlParameter(NextParameterNumber());
            Parameters[parameter.Name] = parameter;
            return parameter;
        }

        private int NextParameterNumber()
        {
            return Parameters.Any()
                       ? Parameters.Values.Max(p => p.Number) + 1
                       : 1;
        }
    }
}