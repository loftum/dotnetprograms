using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using DbTool.Lib.Data;
using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Types;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
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
            var table = (TableMeta) _container.Types.First(t => t.TypeName == "OrderHead");
            var type = _generator.CreateType(table);
            _generator.Save();

            using (var factory = BuildSessionFactoryFor(type))
            {
                using (var session = factory.OpenSession())
                {
                    
                }
            }
        }

        private ISessionFactory BuildSessionFactoryFor(Type type)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c =>
                    c.FromConnectionStringWithKey("Database")).ShowSql())
                .Mappings(m => m.AutoMappings
                    .Add(AutoMap.Assembly(type.Assembly).Conventions.Setup(f => f.Add<IdConvention>()))
                )
                .Diagnostics(d => d.OutputToConsole())
                .BuildSessionFactory();
        }
    }
}