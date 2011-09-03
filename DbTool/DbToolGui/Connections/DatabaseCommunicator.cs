using System;
using System.Data.SqlClient;
using DbTool.Lib.Configuration;
using DbToolGui.Exceptions;
using DbToolGui.ExtensionMethods;

namespace DbToolGui.Connections
{
    public class DatabaseCommunicator : IDatabaseCommunicator
    {
        public string ConnectedTo { get; private set; }

        public bool IsConnected
        {
            get { return _sqlConnection != null; }
        }

        private SqlConnection _sqlConnection;

        public DatabaseCommunicator()
        {
            ConnectedTo = string.Empty;
        }

        public void ConnectTo(DbConnection connectionData)
        {
            if (IsConnected)
            {
                throw new UserException(ExceptionType.AlreadyConnected);
            }
            _sqlConnection = new SqlConnection(connectionData.ConnectionString);
            ConnectedTo = connectionData.Name;
        }

        public void Disconnect()
        {
            if (IsConnected)
            {
                _sqlConnection.Close();
                _sqlConnection.Dispose();
                _sqlConnection = null;
                ConnectedTo = string.Empty;
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
                return new QueryExecutor(_sqlConnection);
            }
            if (statement.StartsWith("migrate"))
            {
                
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
    }
}