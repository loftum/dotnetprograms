using System;

namespace MongoTool.Core.ExtensionMethods
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string[] SplitLines(this string value, bool includeEmptyLines = false)
        {
            var options = includeEmptyLines ? StringSplitOptions.None : StringSplitOptions.RemoveEmptyEntries;
            return value.IsNullOrEmpty()
                ? new string[0]
                : value.Split(new[] { Environment.NewLine }, options);
        }

        public static string GetBlock(this string value, int endIndex)
        {
            if (value.IsNullOrEmpty())
            {
                return value;
            }

            var chars = value.ToCharArray(0, endIndex);
            var foundNewLine = false;
            for (var ii = endIndex - 1; ii > 0; ii--)
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