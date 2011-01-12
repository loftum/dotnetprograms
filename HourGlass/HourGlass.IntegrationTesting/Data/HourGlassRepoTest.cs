using System;
using System.IO;
using HourGlass.Lib.Data;
using HourGlass.Lib.DateAndTime;
using HourGlass.Lib.Domain;
using HourGlass.UnitTesting.Builders;
using NUnit.Framework;

namespace HourGlass.IntegrationTesting.Data
{
    [TestFixture]
    public class HourGlassRepoTest
    {
        private const string DatabaseFilename = "HourGlassTest.db";
        private HourGlassRepo _repo;
        private static readonly DateTime SomeStartDate = new DateTime(1945,05,17);

        [SetUp]
        public void Setup()
        {
            _repo = new HourGlassRepo(SessionFactoryProvider.SqliteSessionFactory(DatabaseFilename, true), new DateProvider());
        }

        [TearDown]
        public void TearDown()
        {
            _repo.Dispose();
            if (File.Exists(DatabaseFilename))
            {
                File.Delete(DatabaseFilename);
            }
        }

        [Test]
        public void ShouldSaveWeek()
        {
            var week = Build.NewWeek().WithStartDate(SomeStartDate).Item;
            var saved = _repo.Save(week);
            Assert.That(saved.IsPersisted);
            Assert.That(week.StartDate, Is.EqualTo(SomeStartDate));
        }

        [Test]
        public void ShouldSaveHourCode()
        {
            var hourCode = Build.NewHourCode().WithCode("CodeA").WithName("NameA").Item;
            _repo.Save(hourCode);
            var saved = _repo.Get<HourCode>(hourCode.Id);
            Assert.That(saved.IsPersisted);
            Assert.That(saved.Code, Is.EqualTo("CodeA"));
            Assert.That(saved.Name, Is.EqualTo("NameA"));
        }

        [Test]
        public void ShouldSaveHourUsage()
        {
            var usage = Build.NewHourUsage()
                .WithMonday(1)
                .WithTuesday(2)
                .WithWednesday(3)
                .WithThursday(4)
                .WithFriday(5)
                .WithSaturday(6)
                .WithSunday(7).Item;
            _repo.Save(usage);

            var saved = _repo.Get<HourUsage>(usage.Id);
            Assert.That(saved.Monday, Is.EqualTo(1));
            Assert.That(saved.Tuesday, Is.EqualTo(2));
            Assert.That(saved.Wednesday, Is.EqualTo(3));
            Assert.That(saved.Thursday, Is.EqualTo(4));
            Assert.That(saved.Friday, Is.EqualTo(5));
            Assert.That(saved.Saturday, Is.EqualTo(6));
            Assert.That(saved.Sunday, Is.EqualTo(7));
        }

        [Test]
        public void ShouldSaveWeekWithUsageAndHourCode()
        {
            var week = Build.NewWeek().WithStartDate(SomeStartDate)
                .WithUsage(Build.NewHourUsage().WithHourCode(Build.NewHourCode().WithCode("CodeA").WithName("NameA")))
                .WithUsage(Build.NewHourUsage().WithHourCode(Build.NewHourCode().WithCode("CodeB").WithName("NameB")))
                .Item;

            _repo.Save(week);

            var gottenWeek = _repo.Get<Week>(week.Id);
            Assert.That(gottenWeek.StartDate, Is.EqualTo(SomeStartDate));
            Assert.That(gottenWeek.Usages.Count, Is.EqualTo(2));

            var usages = gottenWeek.Usages;
            foreach (var hourUsage in usages)
            {
                Assert.That(hourUsage.HourCode, Is.Not.Null);
            }
        }
    }
}
