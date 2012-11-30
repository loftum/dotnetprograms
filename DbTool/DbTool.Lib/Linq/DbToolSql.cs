using System.Collections.Generic;
using System.Text;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Linq
{
    public class DbToolSql
    {
        private readonly StringBuilder _builder;
        public string CommandText { get { return _builder.ToString(); } }
        public IList<DbToolSqlParameter> Parameters { get; set; }

        public DbToolSql()
        {
            _builder = new StringBuilder();
            Parameters = new List<DbToolSqlParameter>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder(CommandText).AppendLine();
            foreach (var parameter in Parameters)
            {
                builder.AppendFormat("{0}='{1}'", parameter.Name, parameter.Value).AppendLine();
            }
            return builder.ToString();
        }

        public DbToolSql Append(string text)
        {
            _builder.Append(text);
            return this;
        }

        public DbToolSql Append(DbToolSql sql)
        {
            _builder.Append(sql.CommandText);
            Parameters.AddRange(sql.Parameters);
            return this;
        }
    }
}