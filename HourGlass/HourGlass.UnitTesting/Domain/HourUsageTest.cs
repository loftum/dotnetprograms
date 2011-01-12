using HourGlass.Lib.Domain;
using NUnit.Framework;

namespace HourGlass.UnitTesting.Domain
{
    [TestFixture]
    public class HourUsageTest
    {
        [Test]
        public void Should()
        {
            var hourCode = new HourCode();
            var hourUsage = new HourUsage();
            hourUsage.SetHourCode(hourCode);

            Assert.That(hourUsage.HourCode, Is.EqualTo(hourCode));
            Assert.That(hourCode.Usages, Contains.Item(hourUsage));
        }
    }
}
