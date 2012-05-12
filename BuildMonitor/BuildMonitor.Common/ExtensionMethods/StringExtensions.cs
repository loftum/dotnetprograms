using System;
using System.Text;

namespace BuildMonitor.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveTrailing(this string value, string trail)
        {
            return value.EndsWith(trail) ? value.Substring(0, value.Length - trail.Length) : value;
        }

        public static string RemoveStarting(this string value, string start)
        {
            return value.StartsWith(start) ? value.Substring(start.Length - 1) : value;
        }

        public static string ToBase64(this string value)
        {
            return Convert.ToBase64String(Encoding.Default.GetBytes(value));
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}