using System.Collections.Generic;
using System.Data;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class AdoExtensions
    {
        public static void AddRange(this IDataParameterCollection collection, IEnumerable<IDbDataParameter> parameters)
        {
            foreach (var parameter in parameters)
            {
                collection.Add(parameter);
            }
        }
    }
}