using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BuildMonitor.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveTrailing(this string value, string trail)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }
            return value.EndsWith(trail) ? value.Substring(0, value.Length - trail.Length) : value;
        }

        public static string RemoveStarting(this string value, string start)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }
            return value.StartsWith(start) ? value.Substring(start.Length - 1) : value;
        }

        public static string ToBase64(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }
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

        public static string RemoveFirstMatch(this string value, string pattern)
        {
            return value.ReplaceFirstMatch(pattern, string.Empty);
        }

        public static string ReplaceFirstMatch(this string value, string pattern, string newValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var regex = new Regex(pattern);
            if (!regex.IsMatch(value))
            {
                return value;
            }

            var match = regex.Match(value);
            return value.Replace(match.Groups[0].Value, newValue);
        }
    }
}