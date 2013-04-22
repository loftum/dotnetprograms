using BasicManifest.Core.Domain;
using BasicManifest.UnitTesting.Builders;
using NUnit.Framework;

namespace BasicManifest.UnitTesting.Core.Domain
{
    [TestFixture]
    public class CampTest
    {
        private Camp _camp;
        private Day _day;
        private Person _instructor;
        private Person _student;
        private Person _student2;

        [SetUp]
        public void Setup()
        {
            _instructor = Build.Instructor().WithMoney(100).Item;
            _student = Build.Student().WithMoney(100).Item;
            _student2 = Build.Student().WithMoney(100).Item;
            _camp = Build.Camp()
                .WithMoney(5000)
                .WithDefaultSlotPrice(20)
                .WithParticipant(_instructor)
                .WithParticipant(_student)
                .WithParticipant(_student2);
            _day = Build.Day().ForCamp(_camp);
        }

        [Test]
        public void Close_GetsPaid_FromSoloJumper()
        {
            var load = Build.Load().ForDay(_day);
            Build.Group()
                .ForLoad(load)
                .WithJumper(_student);
            
            _camp.Close(_day);

            Assert.That(_student.Account.Balance, Is.EqualTo(80m));
        }

        [Test]
        public void Close_GetsPaid_EquallyFromPayers()
        {
            var load = Build.Load().ForDay(_day);
            Build.Group()
                .ForLoad(load)
                .WithJumper(_instructor)
                .WithJumper(_student)
                .WithJumper(_student2);

            _camp.Close(_day);

            Assert.That(_instructor.Account.Balance, Is.EqualTo(100m));
            Assert.That(_student.Account.Balance, Is.EqualTo(70m));
            Assert.That(_student2.Account.Balance, Is.EqualTo(70m));
        }
    }
}