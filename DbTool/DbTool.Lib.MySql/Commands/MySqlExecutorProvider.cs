using System.Data.Common;
using DbTool.Lib.Communication.DbCommands;
using DbTool.Lib.Communication.DbCommands.CSharp;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.MySql.Commands
{
    public class MySqlExecutorProvider : DefaultExecutorProvider
    {
        public MySqlExecutorProvider(IDbToolConfig config, DbToolDatabase database, DbConnection dbConnection, ICSharpExecutor cSharpExecutor)
            : base(config, database, dbConnection, cSharpExecutor)
        {
        }

        public override IDbCommandExecutor GetExecutorFor(string statement)
        {
            if (statement.StartsWithIgnoreCase("show") || statement.StartsWithIgnoreCase("describe"))
            {
                return new SqlExecutor(DbConnection, Settings.MaxResult);
            }
            return base.GetExecutorFor(statement);
        }
    }
}