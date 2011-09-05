using System;
using System.Data.SqlClient;
using DbTool.Lib.Configuration;
using DbToolGui.Communication.Commands;
using DbToolGui.Data;
using DbToolGui.Exceptions;
using DbToolGui.ExtensionMethods;

namespace DbToolGui.Communication
{
    public class DatabaseCommunicator : IDatabaseCommunicator
    {
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
        private readonly IDbToolSettings _settings;

        public DatabaseCommunicator(IDbToolSettings settings)
        {
            _settings = settings;
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
            ThrowIfNotConnected();
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

        private IDbCommandExecutor GetExecutorFor(string statement)
        {
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