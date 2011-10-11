using System.Data.SqlClient;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;

namespace DbTool.Lib.Tasks
{
    public class ViewDbVersionTask : IViewDbVersionTask
    {
        private readonly IDbToolLogger _logger;
        private readonly ConnectionData _connection;

        public ViewDbVersionTask(ConnectionData connection, IDbToolLogger logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public void ViewVersion()
        {
            using (var connection = new SqlConnection(_connection.GetConnectionString()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select max(version) from SchemaInfo";
                try
                {
                    var version = command.ExecuteScalar();
                    _logger.WriteLine("Version of {0}: {1}", _connection.Name, version);
                }
                catch (SqlException e)
                {
                    if (e.Message.Contains("Invalid object name 'SchemaInfo'"))
                    {
                        _logger.WriteLine("No SchemaInfo defined for {0}", _connection.Name);
                    }
                    else
                    {
                        throw;
                    }
                }
                connection.Close();
            }
        }
    }
}