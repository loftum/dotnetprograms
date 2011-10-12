using System.Data.Common;
using DbTool.Lib.Communication.Commands;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.SqlServer.Commands
{
    public class SqlServerExecutorProvider : DefaultExecutorProvider
    {
        public SqlServerExecutorProvider(IDbToolConfig config, ConnectionData connectionData, DbConnection dbConnection)
            : base(config, connectionData, dbConnection)
        {
        }
    }
}