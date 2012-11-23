using System;
using System.Linq;
using System.Reflection;
using DotNetPrograms.Common.Validation;

namespace DotNetPrograms.Common.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static bool In(this object value, params object[] values)
        {
            if (value == null)
            {
                return values.IsNullOrEmpty();
            }
            return values.Any(v => v.Equals(value));
        }

        public static void Set(this object obj, string propertyName, object value)
        {
            if (obj == null)
            {
                return;
            }
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                if (property.Name.EqualsIgnoreCase(propertyName))
                {
                    var converted = value.ConvertTo(property.PropertyType);
                    property.SetValue(obj, converted, new object[0]);
                }
            }
        }

        public static T[] AsArray<T>(this T item)
        {
            return new[] { item };
        }

        public static T ConvertTo<T>(this object value)
        {
            Guard.NotNull(() => value);
            return value == null ? default(T) : (T) Convert.ChangeType(value, typeof(T));
        }
        
        public static object ConvertTo(this object value, Type type)
        {
            Guard.NotNull(() => value);
            return value == null ? null : Convert.ChangeType(value, type);
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