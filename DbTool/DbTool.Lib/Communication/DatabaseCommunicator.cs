using System;
using DbTool.Lib.Communication.Commands;
using DbTool.Lib.Configuration;
using DbTool.Lib.Connections;
using DbTool.Lib.Data;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication
{
    public class DatabaseCommunicator : IDatabaseCommunicator
    {
        public delegate void ResultCallback(IDbCommandResult result);

        public string ConnectedTo
        {
            get { return _connectionData == null ? string.Empty : _connectionData.Name; }
        }

        public bool IsConnected
        {
            get { return _dbContext != null; }
        }

        private readonly IDbToolConfig _config;
        private readonly IDbToolSettings _settings;
        private readonly IDbContextFactory _dbConnectionFactory;

        private ConnectionData _connectionData;
        private DbContext _dbContext;

        public DatabaseCommunicator(IDbToolConfig config, IDbContextFactory dbConnectionFactory)
        {
            _config = config;
            _settings = _config.Settings;
            _dbConnectionFactory = dbConnectionFactory;
        }

        public void ConnectTo(ConnectionData connectionData)
        {
            if (IsConnected)
            {
                throw new UserException(ExceptionType.AlreadyConnected);
            }
            _connectionData = connectionData;
            _dbContext = _dbConnectionFactory.CreateDbContext(connectionData);
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _dbContext.DbConnection.Close();
                _dbContext.DbConnection.Dispose();
                _dbContext = null;
                _connectionData = null;
            }
        }

        public IDbCommandResult Execute(string statement)
        {
            try
            {
                var trimmed = statement.Trim();
                return GetExecutorFor(trimmed).Execute(trimmed);
            }
            catch (Exception e)
            {
                return new ErrorResult(e);
            }
        }

        public void StartExecute(string statement, ResultCallback callback)
        {
            var result = Execute(statement);
            callback(result);
        }

        private IDbCommandExecutor GetExecutorFor(string statement)
        {
            if (statement.StartsWithIgnoreCase("set"))
            {
                return new SetExecutor(_config);
            }
            ThrowIfNotConnected();
            return _dbContext.ExecutorProvider.GetExecutorFor(statement);
        }

        private void ThrowIfNotConnected()
        {
            if (!IsConnected)
            {
                throw new UserException(ExceptionType.NotConnected);
            }
        }

        public Schema LoadSchema()
        {
            ThrowIfNotConnected();
            return new SchemaLoader(_dbContext.DbConnection).Load();
        }
    }
}