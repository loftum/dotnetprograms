using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using MySql.Data.MySqlClient;

namespace DbTool.Lib.MySql.Tasks
{
    public class DeleteDbTask : TaskBase, IDeleteDbTask
    {
        public DeleteDbTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void Delete(string databaseName)
        {
            var connectionString = Settings.DefaultConnection.GetConnectionString();
            Logger.WriteLine("ConnectionString = {0}", connectionString);
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    Logger.Write("Dropping database [{0}] ...", databaseName);
                    command.CommandText = string.Format("drop database {0}", databaseName);
                    command.ExecuteNonQuery();
                    Logger.WriteLine("OK");
                }
                finally
                {
                    Logger.Write("Disconnecting...");
                    connection.Close();
                    Logger.WriteLine("OK");
                }
            }
        }
    }
}