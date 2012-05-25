using System;
using System.Globalization;
using NUnit.Framework;

namespace BuildMonitor.UnitTesting.DateAndTime
{
    [TestFixture]
    public class DateTimeParsingTest
    {
        [Test]
        public void ShouldParseDateTimeWithTimeZone()
        {
            const string dateString = "20120508T082615+0200";
            var date = DateTime.ParseExact(dateString, "yyyyMMddTHHmmsszzz", CultureInfo.InvariantCulture);
            
            Assert.That(date.Year, Is.EqualTo(2012));
            Assert.That(date.Month, Is.EqualTo(5));
            Assert.That(date.Day, Is.EqualTo(8));
            Assert.That(date.Hour, Is.EqualTo(8));
            Assert.That(date.Minute, Is.EqualTo(26));
            Assert.That(date.Second, Is.EqualTo(15));
        }
    }
}