using System.Text.RegularExpressions;

namespace EnvironmentViewer.Lib.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNullOrWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool Matches(this string value, string pattern)
        {
            return Regex.IsMatch(value, pattern);
        }
    }
}