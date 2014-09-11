using System;
using System.Data;

namespace DataAccess.Sql.Stupid
{
    public class ConnectionStatePreserver : IDisposable
    {
        private readonly bool _wasOpen;
        private readonly IDbConnection _connection;

        public ConnectionStatePreserver(IDbConnection connection)
        {
            _connection = connection;
            _wasOpen = connection.State == ConnectionState.Open;
        }

        public void Dispose()
        {
            if (!_wasOpen)
            {
                _connection.Close();
            }
        }
    }
}