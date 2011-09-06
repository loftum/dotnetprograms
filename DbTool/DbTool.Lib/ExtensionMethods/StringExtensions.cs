using System;

namespace DbTool.Lib.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static string ValueOrEmpty(this string value)
        {
            return value ?? string.Empty;
        }

        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        public static bool EqualsIgnoreCase(this string value, string other)
        {
            return value.ToLowerInvariant().Equals(other.ToLowerInvariant());
        }

        public static bool StartsWithIgnoreCase(this string value, string content)
        {
            if (value == null)
            {
                return content == null;
            }
            return value.ToLowerInvariant().StartsWith(content.ToLowerInvariant());
        }

        public static string[] SplitByWhitespace(this string value)
        {
            if (value == null)
            {
                return new string[0];
            }
            return value.Split(new[] {' ', '\n', '\t'}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}