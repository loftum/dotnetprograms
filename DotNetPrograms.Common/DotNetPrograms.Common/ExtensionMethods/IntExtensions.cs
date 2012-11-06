using System;
using System.Globalization;

namespace DotNetPrograms.Common.ExtensionMethods
{
    public static class IntExtensions
    {
        public static string ToInvariantString(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static TimeSpan Minutes(this int value)
        {
            return TimeSpan.FromMinutes(value);
        }
    }
}