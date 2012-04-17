using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VisualFarmStudio.Common.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object item)
        {
            return item == null ? default(T) : (T) Convert.ChangeType(item, typeof (T));
        }

        public static T[] AsArray<T>(this T item)
        {
            return new[]{item};
        }

        public static string ToJson(this object item, bool indented = false)
        {
            if (item == null)
            {
                return string.Empty;
            }

            var settings = new JsonSerializerSettings();
            settings.Error += SuppressError;

            return indented
                       ? JsonConvert.SerializeObject(item, Formatting.Indented, settings)
                       : JsonConvert.SerializeObject(item, Formatting.None, settings);
        }

        private static void SuppressError(object sender, ErrorEventArgs e)
        {
            e.ErrorContext.Handled = true;
        }
    }
}