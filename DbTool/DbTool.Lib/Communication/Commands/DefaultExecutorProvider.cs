using System.Data.Common;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.Commands
{
    public class DefaultExecutorProvider : IExecutorProvider
    {
        private readonly IDbToolConfig _config;
        protected readonly IDbToolSettings Settings;
        protected readonly DbToolDatabase Database;
        protected readonly DbConnection DbConnection;

        public DefaultExecutorProvider(IDbToolConfig config, DbToolDatabase database, DbConnection dbConnection)
        {
            _config = config;
            Settings = _config.Settings;
            Database = database;
            DbConnection = dbConnection;
        }

        public virtual IDbCommandExecutor GetExecutorFor(string statement)
        {
            if (statement.StartsWithIgnoreCase("select"))
            {
                return new QueryExecutor(DbConnection, Settings.MaxResult);
            }
            if (statement.StartsWithIgnoreCase("migrate"))
            {
                return new MigrationExecutor(Database);
            }
            if (statement.StartsWithIgnoreCase("getschema"))
            {
                return new SchemaExecutor(DbConnection);
            }
            if (statement.StartsWithIgnoreCase("backup"))
            {
                return new BackupExecutor();
            }
            return new NonQueryExecutor(DbConnection);
        }
    }
}