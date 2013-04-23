using BasicManifest.Core.Domain;
using BasicManifest.Data.Repositories;
using BasicManifest.Data.Transactions;
using BasicManifest.IntegrationTesting.Ioc;
using BasicManifest.UnitTesting.Builders;
using BasicManifest.UnitTesting.Testdata;
using BasicManifest.Web.Ioc;
using NUnit.Framework;

namespace BasicManifest.IntegrationTesting.Domain
{
    [TestFixture]
    public class BMRepoTest
    {
        private BMRepo _repo;
        private NHibernateUnitOfWork _unitOfWork;

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
            _unitOfWork = ObjectContainer.Get<NHibernateUnitOfWork>();
        }

        [Test]
        public void Delete()
        {
            var camps = _repo.GetAll<Camp>();
            foreach (var camp in camps)
            {
                _repo.Delete(camp);
            }
            _repo.SaveChanges();
        }

        [Test]
        public void Should()
        {
            using (var work = _unitOfWork.Begin())
            {
                var camp = new Camp
                {
                    Name = Some.Name,
                    DefaultSlotPrice = Some.Price
                };
                _repo.Save(camp);

                var instructor = Build.Instructor().WithFirstName("Chris").WithLastName("Rolsdorph");//.WithMoney(500);
                camp.Add(instructor);
                _repo.Save(instructor.Item);
                var student = Build.Student().WithFirstName("Jonas").WithLastName("Hansen").WithMoney(200);
                camp.Add(student);
                _repo.Save(student.Item);
                
                work.Complete();
            }
            _repo.SaveChanges();
        }
    }
}