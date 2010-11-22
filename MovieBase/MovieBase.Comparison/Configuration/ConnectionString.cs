using System.Configuration;

namespace MovieBase.Common.Configuration
{
    public class ConnectionString
    {
        public static string FromConfigByName(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }
    }
}