using System;
using System.Reflection;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class PropertyInfoExtensions
    {
        public static bool HasSetter(this PropertyInfo property)
        {
            return property.GetSetMethod() != null;
        }

        public static object GetDbValue(this PropertyInfo property, object item)
        {
            var value = property.GetValue(item);
            return value ?? DBNull.Value;
        }
    }
}