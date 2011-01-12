using HourGlass.Lib.Domain;
using NUnit.Framework;

namespace HourGlass.UnitTesting.Domain
{
    [TestFixture]
    public class WeekTest
    {
        [Test]
        public void ShouldSetWeekToSelfWhenAddingUsage()
        {
            var week = new Week();
            var usage = new HourUsage();
            week.AddUsage(usage);
            Assert.That(week.Usages.Count, Is.EqualTo(1));
            Assert.That(usage.Week, Is.EqualTo(week));
        }
    }
}
