using System.Configuration;

namespace StuffLibrary.Common.Configuration
{
    public class StuffLibraryConfig : IStuffLibraryConfig
    {
        private static StuffLibraryConfig _instance;
        public static StuffLibraryConfig Instance
        {
            get { return _instance ?? (_instance = new StuffLibraryConfig()); }
        }

        public string Databaseprovider
        {
            get { return GetAppSetting("DatabaseProvider"); }
        }

        public string ConnectionString
        {
            get { return GetConnectionString("StuffLibraryBase").ConnectionString; }
        }

        private static ConnectionStringSettings GetConnectionString(string name)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[name];
            return connectionString;
        }

        private static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}