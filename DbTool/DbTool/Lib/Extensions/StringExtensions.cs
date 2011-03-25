using System.Linq;
using System.Text;

namespace DbTool.Lib.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsOneOf(this string value, params string[] otherValues)
        {
            return otherValues.Any(value.Equals);
        }

        public static string InitCap(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var firstLetter = value.Substring(0, 1).ToUpperInvariant();
            return new StringBuilder(firstLetter).Append(value.Substring(1)).ToString();
        }

        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return value.ToLowerInvariant().Equals(other.ToLowerInvariant());
        }
    }
}