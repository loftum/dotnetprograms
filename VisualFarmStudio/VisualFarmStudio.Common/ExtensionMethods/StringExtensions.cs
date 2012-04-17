using System.Text.RegularExpressions;
using VisualFarmStudio.Common.Validation;

namespace VisualFarmStudio.Common.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string RemoveFirstMatch(this string value, string pattern)
        {
            Guard.NotNull(() => value);
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

        public static bool Matches(this string value, string pattern)
        {
            Guard.NotNull(() => value);
            return Regex.IsMatch(value, pattern);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}