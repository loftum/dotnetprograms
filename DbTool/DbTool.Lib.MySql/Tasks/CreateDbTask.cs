using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using MySql.Data.MySqlClient;

namespace DbTool.Lib.MySql.Tasks
{
    public class CreateDbTask : TaskBase, ICreateDbTask
    {
        public CreateDbTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void Create(string databaseName)
        {
            var connectionString = Settings.DefaultConnection.GetConnectionString(false);
            using (var connection = new MySqlConnection(connectionString))
            {
                
                try
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    Logger.Write("Creating database [{0}] ...", databaseName);
                    command.CommandText = string.Format("create database {0}", databaseName);
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