using System;
using System.Linq;

namespace Wordbank.Lib.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static void ShouldNotBeNull(this object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static T[] AsArray<T>(this T item)
        {
            return new[] {item};
        }

        public static bool In<T>(this T item, params T[] other)
        {
            return other.Any(o => o.Equals(item));
        }

        public static T ConvertTo<T>(this object obj)
        {
            return (T)obj.ConvertTo(typeof(T));
        }

        public static object ConvertTo(this object value, Type type)
        {
            if (value.GetType() == type)
            {
                return value;
            }
            if (type == typeof(int))
            {
                return Convert.ToInt32(value);
            }
            if (type == typeof(long))
            {
                return Convert.ToInt64(value);
            }
            if (type == typeof(double))
            {
                return Convert.ToDouble(value);
            }
            if (type == typeof(bool))
            {
                return Convert.ToBoolean(value);
            }
            if (type == typeof(string))
            {
                return Convert.ToString(value);
            }
            if (type == typeof(DateTime))
            {
                return Convert.ToDateTime(value);
            }
            throw new FormatException(string.Format("Cannot convert {0} to {1}", value, type));
        }
    }
}