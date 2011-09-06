using System.Data;

namespace DbTool.Lib.ExtensionMethods
{
    public static class DataRowExtensions
    {
        public static T Get<T>(this DataRow row, string column)
        {
            return (T) row[column];
        }
    }
}