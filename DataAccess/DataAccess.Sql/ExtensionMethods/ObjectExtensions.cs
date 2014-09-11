using System;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static object ToDbValue(this object value)
        {
            return value ?? DBNull.Value;
        }
    }
}