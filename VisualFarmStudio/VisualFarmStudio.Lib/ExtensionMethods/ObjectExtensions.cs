using System;
using Newtonsoft.Json;

namespace VisualFarmStudio.Lib.ExtensionMethods
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
            return indented
                       ? JsonConvert.SerializeObject(item, Formatting.Indented)
                       : JsonConvert.SerializeObject(item, Formatting.None);
        }
    }
}