using System.Collections.Generic;
using System.Data.SqlClient;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Logging;

namespace DbTool.Lib.Tasks
{
    public class DatabaseVersionViewer : TaskBase
    {
        public DatabaseVersionViewer(IDbToolLogger logger, IDbToolSettings settings)
            : base("version", "<database>", "MyDatabase", logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 1;
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];
            if (!Settings.HasConnectionString(databaseName))
            {
                throw new DbToolException("No connection for " + databaseName + " is defined.");
            }
            var connectionString = Settings.GetConnectionString(databaseName);
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "select max(version) from SchemaInfo";
                try
                {
                    var version = command.ExecuteScalar();
                    Logger.WriteLine("Version of {0}: {1}", databaseName, version);
                }
                catch (SqlException e)
                {
                    if (e.Message.Contains("Invalid object name 'SchemaInfo'"))
                    {
                        Logger.WriteLine("No SchemaInfo defined for {0}", databaseName);
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