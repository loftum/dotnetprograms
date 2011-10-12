using System.Data.Common;
using DbTool.Lib.Communication.Commands;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.MySql.Commands
{
    public class MySqlExecutorProvider : DefaultExecutorProvider
    {
        public MySqlExecutorProvider(IDbToolConfig config, ConnectionData connectionData, DbConnection dbConnection)
            : base(config, connectionData, dbConnection)
        {
        }

        public override IDbCommandExecutor GetExecutorFor(string statement)
        {
            if (statement.StartsWithIgnoreCase("show") || statement.StartsWithIgnoreCase("describe"))
            {
                return new QueryExecutor(DbConnection, Settings.MaxResult);
            }
            return base.GetExecutorFor(statement);
        }
    }
}