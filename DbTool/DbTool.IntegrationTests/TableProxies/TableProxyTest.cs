using System;
using System.Configuration;
using System.Data.SqlClient;
using DbTool.Lib.Data;
using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Types;
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
            //_generator.Save();

            foreach (var type in _generator.Assembly.GetTypes())
            {
                Show(type);
            }
        }

        private void Show(Type type)
        {
            Console.WriteLine(type.Name);
            foreach (var property in type.GetProperties())
            {
                Console.WriteLine(property);
            }
        }
    }
}