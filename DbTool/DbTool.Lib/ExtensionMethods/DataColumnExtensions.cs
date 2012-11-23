using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbTool.Lib.ExtensionMethods
{
    public static class DataColumnExtensions
    {
        public static IEnumerable<string> GetNames(this DataColumnCollection columns)
        {
            return (from DataColumn column in columns select column.ColumnName).ToList();
        }
    }
}