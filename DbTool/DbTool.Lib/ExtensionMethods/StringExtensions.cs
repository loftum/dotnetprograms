using System;
using System.Linq;

namespace DbTool.Lib.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string[] SplitLines(this string value)
        {
            return value.IsNullOrEmpty()
                ? new string[0]
                : value.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static bool IsSingleWord(this string value)
        {
            value.ShouldNotBeNull("value");
            return value.Matches(@"^[\S]+$");
        }

        public static void ShouldNotBeNullOrWhitespace(this string value, string name)
        {
            if (value.IsNullOrWhitespace())
            {
                throw new ArgumentNullException(name);
            }
        }

        public static bool IsNullOrWhitespace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

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

        public static string TrimEndingWhitespaces(this string value)
        {
            value.ShouldNotBeNull("value");
            return value.TrimEnd(null);
        }

        public static string ToCamelCase(this string value)
        {
            if (value.IsNotNullOrEmpty())
            {
                return value;
            }
            var parts = value.Split('_').Select(v => v.InitCap());
            return string.Join(string.Empty, parts);
        }

        public static string InitCap(this string value)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }
            var firstLetter = value.Substring(0, 1).ToUpperInvariant();
            var theRest = value.Substring(1);
            return string.Format("{0}{1}", firstLetter, theRest);
        }

        public static string GetBlock(this string value, int endIndex)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            var chars = value.ToCharArray(0, endIndex + 1);
            var foundNewLine = false;
            for(var ii = endIndex; ii > 0; ii--)
            {
                if (chars[ii] == '\n')
                {
                    if (foundNewLine)
                    {
                        return value.Substring(ii, endIndex - ii).TrimStart();
                    }
                    foundNewLine = true;
                }
            }
            return value.Substring(0, endIndex);
        }
    }
}