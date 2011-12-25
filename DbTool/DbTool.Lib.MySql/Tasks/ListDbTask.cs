using System.Data;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using MySql.Data.MySqlClient;

namespace DbTool.Lib.MySql.Tasks
{
    public class ListDbTask : TaskBase, IListDbTask
    {
        public ListDbTask(IDbToolLogger logger, IDbToolSettings settings) : base(logger, settings)
        {

        }

        public void ListDatabases()
        {
            var connectionString = Settings.DefaultConnection.GetConnectionString();
            using (var connection = new MySqlConnection(connectionString))
            {
                Logger.WriteLine("Databases:");
                var databases = connection.GetSchema("Tables");
                foreach (DataRow database in databases.Rows)
                {
                    Logger.WriteLine("Name: {0}", database["database_name"]);
                }
            }
        }
    }
}