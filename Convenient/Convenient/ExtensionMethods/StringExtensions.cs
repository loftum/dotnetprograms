using System;
using System.Text.RegularExpressions;

namespace Convenient.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveFirstMatch(this string value, string pattern)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
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

        public static string NullSafeTrim(this string value)
        {
            return value == null ? null : value.Trim();
        }
    }
}