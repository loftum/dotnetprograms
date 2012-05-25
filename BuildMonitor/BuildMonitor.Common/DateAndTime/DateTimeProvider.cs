using System;
using System.Globalization;
using BuildMonitor.Common.ExtensionMethods;

namespace BuildMonitor.Common.DateAndTime
{
    public static class DateTimeProvider
    {
        public static DateTime ParseWithTimeZone(string value)
        {
            return value.IsNullOrWhiteSpace()
                ? new DateTime(1900, 01, 01)
                : DateTime.ParseExact(value, "yyyyMMddTHHmmsszzz", CultureInfo.InvariantCulture);
        }
    }
}