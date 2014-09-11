using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class DataRecordExtensions
    {
        public static string[] GetColumns(this IDataRecord record)
        {
            return EnumerateColumns(record).ToArray();
        }

        private static IEnumerable<string> EnumerateColumns(IDataRecord record)
        {
            for (var ii = 0; ii < record.FieldCount; ii++)
            {
                yield return record.GetName(ii);
            }
        }
    }
}