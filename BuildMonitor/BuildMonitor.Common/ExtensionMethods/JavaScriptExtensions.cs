using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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

        public static string ToJson(this object value, bool indented = false, bool suppressError = false)
        {
            var settings = new JsonSerializerSettings
                {
                    Error = SuppressError,
                    Formatting = indented ? Formatting.Indented : Formatting.None
                };
            return JsonConvert.SerializeObject(value, settings);
        }

        public static T FromJsonTo<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        private static void SuppressError(object sender, ErrorEventArgs e)
        {
            e.ErrorContext.Handled = true;
        }
    }
}