using System.Linq;
using System.Text.RegularExpressions;

namespace EnvironmentViewer.Lib.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool Matches(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }

        public static bool EqualsOneOf(this string value, params string[] otherValues)
        {
            return otherValues.Any(value.Equals);
        }

        public static bool StartsWithOneOf(this string value, params string[] otherValues)
        {
            return otherValues.Any(value.StartsWith);
        }
    }
}