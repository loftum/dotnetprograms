using System;
using System.Reflection;

namespace DbTool.Lib.ExtensionMethods
{
    public static class ObjectExtensions
    {
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

        public static T ConvertTo<T>(this object obj)
        {
            return (T) obj.ConvertTo(typeof (T));
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

        public static void ShouldNotBeNull(this object obj, string name = null)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}