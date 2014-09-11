using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.Sql.Linq.Statements;

namespace DataAccess.Sql.Statements
{
    public class InsertStatement : ISqlStatement
    {
        public string CommandText { get; private set; }
        public IList<SqlParameter> Parameters { get; private set; }

        public static InsertStatement For<T>(T item)
        {
            return For(item, typeof (T).Name);
        }

        public static InsertStatement For<T>(T item, string table)
        {
            return new InsertStatement(item, table);
        }

        private InsertStatement(object item, string table)
        {
            var template = InsertStatementTemplate.For(item.GetType(), table);
            CommandText = template.CommandText;
            Parameters = template.CreateParametersFor(item).ToList();
        }
    }
}