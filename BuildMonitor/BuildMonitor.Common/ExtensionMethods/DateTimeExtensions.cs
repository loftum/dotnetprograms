using System;
using System.Globalization;

namespace BuildMonitor.Common.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static DateTime ToDateTime(this string value)
        {
            return value.IsNullOrWhiteSpace()
                ? new DateTime(1900, 01, 01)
                : DateTime.ParseExact(value, "yyyyMMddTHHmmsszzz", CultureInfo.InvariantCulture);
        }
    }
}