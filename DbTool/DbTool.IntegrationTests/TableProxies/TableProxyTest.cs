using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using DbTool.Lib.Data;
using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Emit;
using DbTool.Lib.Meta.Types;
using NUnit.Framework;

namespace DbTool.IntegrationTests.TableProxies
{
    [TestFixture]
    public class TableProxyTest
    {
        private DatabaseSchema _schema;
        private TableTypeGenerator _generator;

        [TestFixtureSetUp]
        public void Setup()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Database"].ConnectionString))
            {
                _schema = new SchemaLoader(connection).Load();
            }
            _generator = new TableTypeGenerator(_schema.FullName);
        }

        [Test]
        public void ShowTypes()
        {
            foreach (var table in _schema.Tables)
            {
                _generator.CreateType(table);
            }

            foreach (var type in _generator.Assembly.GetTypes())
            {
                Show(type);
            }
        }

        [Test]
        public void CreateTypeFromOtherAppDomain()
        {
            foreach (var table in _schema.Tables)
            {
                _generator.CreateType(table);
            }
//            foreach (var type in _generator.Assembly.GetTypes())
//            {
//                Show(type);
//            }

            var setup = new AppDomainSetup {ApplicationBase = Environment.CurrentDirectory};

            var domain = AppDomain.CreateDomain("Stuff", null, setup);
            domain.Load(_generator.DynamicAssembly.GetBytes());
            Show(domain);

            var orderHead =  domain.CreateInstance(_generator.Assembly.FullName, "dbo.Hapi.OrderHead");
            
            Show(_generator.Assembly.GetTypes().First(t => t.Name == "OrderHead"));
            var o = orderHead.Unwrap();
            

            AppDomain.Unload(domain);
        }

        [Test]
        public void Should()
        {
            var assembly = new DynamicAssembly("noe");
            var type = assembly.BuildClass("Hest").WithAttribute<SerializableAttribute>().CreateType();
            

            var setup = new AppDomainSetup { ApplicationBase = Environment.CurrentDirectory };
            var domain = AppDomain.CreateDomain("Stuff", null, setup);
            Show(domain);
            domain.Load(assembly.GetBytes());

            var hest = domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
        }


        private void Show(AppDomain domain)
        {
            Console.WriteLine(domain.FriendlyName);
            foreach (var assembly in domain.GetAssemblies())
            {
                Show(assembly);
            }
        }

        private void Show(Assembly assembly)
        {
            Console.WriteLine(assembly.FullName);
        }

        private void Show(Type type)
        {
            foreach (var attribute in type.GetCustomAttributes(false))
            {
                Console.WriteLine("[{0}]", attribute);
            }
            Console.WriteLine(type.Name);
            foreach (var property in type.GetMembers())
            {
                Console.WriteLine("- {0}", property);
            }
        }
    }
}