using System;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string InBrackets(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return string.Format("[{0}]", value);
        }

        public static string ToParameterName(this string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            return string.Format("@{0}", value);
        }
    }
}