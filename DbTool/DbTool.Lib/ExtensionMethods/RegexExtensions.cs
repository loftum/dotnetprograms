using System.Text.RegularExpressions;

namespace DbTool.Lib.ExtensionMethods
{
    public static class RegexExtensions
    {
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

        public static bool Matches(this string value, string pattern)
        {
            value.ShouldNotBeNull("value");
            pattern.ShouldNotBeNull("pattern");
            return Regex.IsMatch(value, pattern);
        }
    }
}