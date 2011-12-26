using System.Data.Common;
using DbTool.Lib.Communication.DbCommands;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.SqlServer.Commands
{
    public class SqlServerExecutorProvider : DefaultExecutorProvider
    {
        public SqlServerExecutorProvider(IDbToolConfig config, DbToolDatabase database, DbConnection dbConnection)
            : base(config, database, dbConnection)
        {
        }
    }
}