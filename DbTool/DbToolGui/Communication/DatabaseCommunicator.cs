using System;
using System.Data.SqlClient;
using DbTool.Lib.Configuration;
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
            get { return _sqlConnection != null; }
        }

        private ConnectionData _connectionData;
        private SqlConnection _sqlConnection;
        private readonly IDbToolConfig _config;
        private readonly IDbToolSettings _settings;

        public DatabaseCommunicator(IDbToolConfig config)
        {
            _config = config;
            _settings = _config.Settings;
        }

        public void ConnectTo(ConnectionData connectionData)
        {
            if (IsConnected)
            {
                throw new UserException(ExceptionType.AlreadyConnected);
            }
            _connectionData = connectionData;
            _sqlConnection = new SqlConnection(connectionData.ConnectionString);
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
                _sqlConnection = null;
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
                return new QueryExecutor(_sqlConnection, _settings.MaxResult);
            }
            if (statement.StartsWithIgnoreCase("migrate"))
            {
                return new MigrationExecutor(_connectionData);
            }
            if (statement.StartsWithIgnoreCase("getschema"))
            {
                return new SchemaExecutor(_sqlConnection);
            }
            if (statement.StartsWithIgnoreCase("backup"))
            {
                return new BackupExecutor();
            }
            return new NonQueryExecutor(_sqlConnection);
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
            return new SchemaLoader(_sqlConnection).Load();
        }
    }
}