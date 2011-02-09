using EnvironmentViewer.Lib.Exceptions;
using EnvironmentViewer.Lib.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace EnvironmentViewer.Lib.SessionFactories
{
    public class VersionSessionFactoryProvider : IVersionSessionFactoryProvider
    {
        public ISessionFactory Create(DatabaseCredentials credentials)
        {
            switch(credentials.DatabaseType)
            {
                case "mysql":
                    return CreateForMysql(credentials);
                case "sqlserver":
                    return CreateForSqlServer(credentials);
                default:
                    throw new UserException(ExceptionType.InvalidDatabaseType, credentials.DatabaseType);
            }
        }

        private static ISessionFactory CreateForSqlServer(DatabaseCredentials credentials)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(credentials.BuildConnectionString()))
                .Mappings(MappingForSchemaInfo)
                .BuildSessionFactory();
        }

        private static ISessionFactory CreateForMysql(DatabaseCredentials credentials)
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString(credentials.BuildConnectionString()))
                .Mappings(MappingForSchemaInfo)
                .BuildSessionFactory();
        }

        private static void MappingForSchemaInfo(MappingConfiguration m)
        {
            m.FluentMappings.Add<SchemaInfoMap>();
        }
    }
}