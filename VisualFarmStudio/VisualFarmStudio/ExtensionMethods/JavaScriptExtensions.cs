using System.Globalization;

namespace VisualFarmStudio.ExtensionMethods
{
    public static class JavaScriptExtensions
    {
        public static string ToJavaScript(this object value)
        {
            return string.Format("\"{0}\"", value);
        }

        public static string ToJavaScript(this bool value)
        {
            return value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
        }
    }
}