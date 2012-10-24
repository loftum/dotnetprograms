using System.Data.Common;
using DbTool.Lib.Communication.DbCommands;
using DbTool.Lib.Communication.DbCommands.CSharp;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.SqlServer.Commands
{
    public class SqlServerExecutorProvider : DefaultExecutorProvider
    {
        public SqlServerExecutorProvider(IDbToolConfig config, DbToolDatabase database, DbConnection dbConnection, ICSharpExecutor cSharpExecutor)
            : base(config, database, dbConnection, cSharpExecutor)
        {
        }
    }
}