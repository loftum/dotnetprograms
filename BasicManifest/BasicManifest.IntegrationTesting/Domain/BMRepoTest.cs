using BasicManifest.Core.Domain;
using BasicManifest.Data.Repositories;
using BasicManifest.IntegrationTesting.Ioc;
using BasicManifest.UnitTesting.Testdata;
using BasicManifest.Web.Ioc;
using NUnit.Framework;

namespace BasicManifest.IntegrationTesting.Domain
{
    [TestFixture]
    public class BMRepoTest
    {
        private BMRepo _repo;

        [TestFixtureSetUp]
        public void SetUpTestFixture()
        {
            Lifecycle.Current = new IntegrationTestLifecycle();
            ObjectContainer.Init(new BMRegistry());
        }

        [SetUp]
        public void Setup()
        {
            Lifecycle.Current = new IntegrationTestLifecycle();
            _repo = ObjectContainer.Get<BMRepo>();
        }

        [Test]
        public void Should()
        {
            var camp = new Camp
                {
                    Name = Some.Name,
                    DefaultSlotPrice = Some.Price
                };
            _repo.Save(camp);
            _repo.SaveChanges();
        }
    }
}