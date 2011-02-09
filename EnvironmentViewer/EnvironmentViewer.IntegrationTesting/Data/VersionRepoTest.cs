using EnvironmentViewer.Lib.Data;
using EnvironmentViewer.Lib.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NUnit.Framework;

namespace EnvironmentViewer.IntegrationTesting.Data
{
    [TestFixture]
    public class VersionRepoTest
    {
        private VersionRepo _repo;

        [SetUp]
        public void Setup()
        {
            var factory = CreateSessionFactory();
            _repo = new VersionRepo(factory);
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString("Server=localhost;Database=HasVersion;Uid=jule;Pwd=nissen;"))
                .Mappings(m => m.FluentMappings.Add<SchemaInfoMap>())
                .BuildSessionFactory();
        }

        [Test]
        public void ShouldGetVersion()
        {
            var version = _repo.GetVersion();
            Assert.That(version, Is.EqualTo(1));
        }
    }
}