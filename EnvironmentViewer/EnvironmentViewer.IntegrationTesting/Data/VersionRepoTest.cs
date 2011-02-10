using EnvironmentViewer.Lib.Data;
using EnvironmentViewer.Lib.Modules;
using EnvironmentViewer.Lib.SessionFactories;
using Ninject;
using NUnit.Framework;

namespace EnvironmentViewer.IntegrationTesting.Data
{
    [TestFixture]
    public class VersionRepoTest
    {
        private IVersionRepo _repo;
        private IKernel _kernel;
        private IVersionRepoProvider _provider;

        [SetUp]
        public void Setup()
        {
            _kernel = new StandardKernel(new RepoModule());
            _provider = _kernel.Get<IVersionRepoProvider>();
            var credentials = new DatabaseCredentials
                                  {
                                      Database = "ETF",
                                      DatabaseType = "sqlserver",
                                      Host = "(local)",
                                      IntegratedSecurity = true
                                  };
            _repo = _provider.GetVersionRepo(credentials);
        }

        [Test]
        public void ShouldGetVersion()
        {
            var version = _repo.GetVersion();
            Assert.That(version, Is.EqualTo(1));
        }
    }
}