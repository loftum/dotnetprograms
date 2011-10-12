using System.Data.Common;
using DbTool.Lib.Communication.Commands;

namespace DbTool.Lib.Connections
{
    public class DbContext
    {
        public DbConnection DbConnection { get; private set; }
        public IExecutorProvider ExecutorProvider { get; private set; }

        public DbContext(DbConnection dbConnection, IExecutorProvider executorProvider)
        {
            DbConnection = dbConnection;
            ExecutorProvider = executorProvider;
        }
    }
}