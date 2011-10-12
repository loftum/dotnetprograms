using System;
using System.Data.Common;
using DbTool.Lib.Configuration;
using DbTool.Lib.Connections;
using DbTool.Lib.ExtensionMethods;
using DbToolGui.Communication.Commands;
using DbToolGui.Data;
using DbToolGui.Exceptions;

namespace DbToolGui.Communication
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
            get { return _dbConnection != null; }
        }

        private ConnectionData _connectionData;
        private DbConnection _dbConnection;
        private readonly IDbToolConfig _config;
        private readonly IDbToolSettings _settings;
        private readonly IConnectionFactory _connectionFactory;

        public DatabaseCommunicator(IDbToolConfig config, IConnectionFactory connectionFactory)
        {
            _config = config;
            _settings = _config.Settings;
            _connectionFactory = connectionFactory;
        }

        public void ConnectTo(ConnectionData connectionData)
        {
            if (IsConnected)
            {
                throw new UserException(ExceptionType.AlreadyConnected);
            }
            _connectionData = connectionData;
            _dbConnection = _connectionFactory.CreateConnection(connectionData);
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _dbConnection.Close();
                _dbConnection.Dispose();
                _dbConnection = null;
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
            if (statement.StartsWithIgnoreCase("select"))
            {
                return new QueryExecutor(_dbConnection, _settings.MaxResult);
            }
            if (statement.StartsWithIgnoreCase("migrate"))
            {
                return new MigrationExecutor(_connectionData);
            }
            if (statement.StartsWithIgnoreCase("getschema"))
            {
                return new SchemaExecutor(_dbConnection);
            }
            if (statement.StartsWithIgnoreCase("backup"))
            {
                return new BackupExecutor();
            }
            return new NonQueryExecutor(_dbConnection);
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
            return new SchemaLoader(_dbConnection).Load();
        }
    }
}