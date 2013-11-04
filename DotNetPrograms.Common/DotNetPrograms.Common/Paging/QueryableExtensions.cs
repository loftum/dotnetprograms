using System;
using System.Linq;

namespace DotNetPrograms.Common.Paging
{
    public static class QueryableExtensions
    {
        public static PagedList<TOut> Paged<TOut>(this IQueryable<TOut> query, int pageNumber, int pageSize)
        {
            return new PagedList<TOut>(query, pageNumber, pageSize);
        }

        public static PagedList<TOut> Paged<TIn, TOut>(this IQueryable<TIn> query, int pageNumber, int pageSize, Func<TIn, TOut> map)
        {
            return PagedList<TOut>.Create(query, pageNumber, pageSize, map);
        }
    }
}