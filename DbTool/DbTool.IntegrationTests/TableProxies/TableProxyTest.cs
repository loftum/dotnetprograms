using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using DbTool.Lib.Data;
using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Types;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace DbTool.IntegrationTests.TableProxies
{
    [TestFixture]
    public class TableProxyTest
    {
        private TypeContainer _container;
        private TableTypeGenerator _generator;

        [TestFixtureSetUp]
        public void Setup()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                _container = new SchemaLoader(connection).Load();
            }
            _generator = new TableTypeGenerator(_container.Name);
        }

        [Test]
        public void Should()
        {
            foreach (var table in _container.TableTypes)
            {
                _generator.CreateType(table);
            }
            _generator.Save();

            using (var factory = BuildSessionFactoryFor(_generator.Assembly))
            {
                using (var session = factory.OpenSession())
                {
                    session.Query<SchemaLoader>().Where(l => l.GetType() == typeof(object));
                }
            }
        }

        private static void Show(Type type)
        {
            foreach (var property in type.GetProperties())
            {
                Console.WriteLine("{0} {1}", property.PropertyType, property.Name);
            }
        }

        private static ISessionFactory BuildSessionFactoryFor(Assembly assembly)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c =>
                    c.FromConnectionStringWithKey("Database")).ShowSql())
                .Mappings(m => m.AutoMappings
                    .Add(AutoMap.Assembly(assembly).Conventions.Setup(f => f.Add<IdConvention>()))
                )
                .BuildSessionFactory();
        }
    }
}