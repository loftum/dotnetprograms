using System.Data.Common;
using DbTool.Lib.Communication.DbCommands.CSharp;
using DbTool.Lib.Communication.DbCommands.DbSchema;
using DbTool.Lib.Configuration;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands
{
    public class DefaultExecutorProvider : IExecutorProvider
    {
        private readonly IDbToolConfig _config;
        protected readonly IDbToolSettings Settings;
        protected readonly DbToolDatabase Database;
        protected readonly DbConnection DbConnection;
        private readonly ICSharpExecutor _cSharpExecutor;

        public DefaultExecutorProvider(IDbToolConfig config, DbToolDatabase database, DbConnection dbConnection, ICSharpExecutor cSharpExecutor)
        {
            _config = config;
            Settings = _config.Settings;
            Database = database;
            DbConnection = dbConnection;
            _cSharpExecutor = cSharpExecutor;
        }

        public virtual IDbCommandExecutor GetExecutorFor(string statement)
        {
            if (statement.StartsWithIgnoreCase("select"))
            {
                return new SqlExecutor(DbConnection, Settings.MaxResult);
            }
            if (statement.StartsWithIgnoreCase("insert") ||
                statement.StartsWithIgnoreCase("update") ||
                statement.StartsWithIgnoreCase("delete") ||
                statement.StartsWithIgnoreCase("drop") ||
                statement.StartsWithIgnoreCase("create"))
            {
                return new NonQueryExecutor(DbConnection);
            }
            if (statement.StartsWithIgnoreCase("show"))
            {
                return new SchemaExecutor(DbConnection);
            }
            if (statement.StartsWithIgnoreCase("backup"))
            {
                return new BackupExecutor();
            }
            _cSharpExecutor.Db = Database;
            _cSharpExecutor.DbConnection = DbConnection;
            return _cSharpExecutor;
        }
    }
}