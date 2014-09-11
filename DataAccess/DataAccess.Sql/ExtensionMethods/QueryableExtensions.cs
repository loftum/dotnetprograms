using System.Linq;
using DataAccess.Sql.Query;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class QueryableExtensions
    {
        public static int Delete<T>(this IQueryable<T> queryable)
        {
            var provider = (IDeleteProvider)queryable.Provider;
            return provider.ExecuteDelete(queryable.Expression);
        }
    }
}