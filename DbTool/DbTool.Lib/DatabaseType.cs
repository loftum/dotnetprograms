using DbTool.Lib.Exceptions;
using MigSharp;

namespace DbTool.Lib
{
    public class DatabaseType
    {
        public const string SqlServer = "sqlserver";
        public const string SqlServer2005 = "sqlserver2005";
        public const string SqlServer2008 = "sqlserver2008";
        public const string MySql = "mysql";

        public static string ToMigSharpProvider(string databaseType)
        {
            switch(databaseType.ToLowerInvariant())
            {
                case SqlServer:
                case SqlServer2008:
                    return ProviderNames.SqlServer2008;
                default:
                    throw new UserException(ExceptionType.UnsupportedMigSharpProviderName, databaseType);
            }
        }
    }
}