using System.Data;
using DataAccess.Sql.Linq.Statements;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class SqlStatementExtensions
    {
        public static void MapFrom(this IDbCommand command, ISqlStatement statement)
        {
            command.CommandText = statement.CommandText;
            command.Parameters.AddRange(statement.Parameters);
        }
    }
}