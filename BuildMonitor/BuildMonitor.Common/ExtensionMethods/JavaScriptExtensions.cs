using System.Globalization;

namespace BuildMonitor.Common.ExtensionMethods
{
    public static class JavaScriptExtensions
    {
        public static string ToJs(this bool value)
        {
            return value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        }

        public static string ToJs(this object value)
        {
            return string.Format("\"{0}\"", value);
        }
    }
}