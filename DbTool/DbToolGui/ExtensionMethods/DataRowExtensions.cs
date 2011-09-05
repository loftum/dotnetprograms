using System.Data;

namespace DbToolGui.ExtensionMethods
{
    public static class DataRowExtensions
    {
        public static T Get<T>(this DataRow row, string column)
        {
            return (T) row[column];
        }
    }
}