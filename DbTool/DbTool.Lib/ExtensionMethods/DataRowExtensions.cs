using System.Collections.Generic;
using System.Data;

namespace DbTool.Lib.ExtensionMethods
{
    public static class DataRowExtensions
    {
        public static T Get<T>(this DataRow row, string column)
        {
            return (T) row[column];
        }

        public static T Get<T>(this IDataRecord row, string column)
        {
            return (T)row[column];
        }

        public static IDictionary<string, object> ToDictionary(this DataRow row)
        {
            var dictionary = new Dictionary<string, object>();
            var columns = row.Table.Columns.GetNames();
            foreach (var column in columns)
            {
                dictionary[column] = row[column];
            }
            return dictionary;
        }
    }
}