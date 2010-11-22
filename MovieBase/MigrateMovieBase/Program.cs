using System.Configuration;
using MovieBase.Migrations;

namespace MigrateMovieBase
{
    public class Program
    {
        static void Main(string[] args)
        {
            MigrateDatabase();
        }

        private static void MigrateDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["MovieBase"].ConnectionString;
            new MovieBaseMigrator(connectionString).MigrateToLatest();
        }
    }
}
