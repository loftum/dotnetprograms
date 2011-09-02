using System.Data.SqlClient;
using DbTool.Lib.Configuration;
using DbToolGui.Exceptions;

namespace DbToolGui.Connections
{
    public class DatabaseCommunicator
    {
        private SqlConnection _sqlConnection;

        public void ConnectTo(DbConnection connectionData)
        {
            if (IsConnected)
            {
                throw new UserException(ExceptionType.AlreadyConnected);
            }
            _sqlConnection = new SqlConnection(connectionData.ConnectionString);
        }
    }
}