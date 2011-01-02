using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using HourGlass.Lib.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace HourGlass.Lib.Data
{
    public class SessionFactoryProvider
    {
        public static ISessionFactory SqliteSessionFactory(string filename)
        {
            var configuration = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile(filename))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Week>());
            
            if (!File.Exists(filename))
            {
                configuration.ExposeConfiguration(BuildSchema);
            }
            //configuration.ExposeConfiguration(BuildSchema);

            return configuration.BuildSessionFactory();
        }

        private static void BuildSchema(Configuration configuration)
        {
            new SchemaExport(configuration).Create(false, true);
        }
    }
}