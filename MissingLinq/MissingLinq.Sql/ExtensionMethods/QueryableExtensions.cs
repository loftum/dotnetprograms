using System.Linq;

namespace MissingLinq.Sql.ExtensionMethods
{
    public static class QueryableExtensions
    {
        public static void Delete<T>(this IQueryable<T> queryable)
        {
            var query = (MissingLinqQueryable<T>) queryable;
            var provider = (MissingLinqQueryProvider) query.Provider;
            provider.ExecuteDelete<T>(queryable.Expression);
        }
    }
}