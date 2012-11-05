using System;
using DotNetPrograms.Common.Validation;

namespace DotNetPrograms.Common.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static T[] AsArray<T>(this T item)
        {
            return new[] { item };
        }

        public static T ConvertTo<T>(this object value)
        {
            Guard.NotNull(() => value);
            return value == null ? default(T) : (T) Convert.ChangeType(value, typeof(T));
        }

        public static T ConvertToOrDefault<T>(this object value, T defaultValue = default(T))
        {
            Guard.NotNull(() => value);
            try
            {
                return value.ConvertTo<T>();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}