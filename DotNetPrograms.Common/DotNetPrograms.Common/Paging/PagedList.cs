using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPrograms.Common.Paging
{
    public class PagedList<T> : IPagedList<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int ItemCount { get; private set; }
        public int TotalItemCount { get; private set; }

        public Pager Pager { get; private set; }

        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize)
            : this(items.AsQueryable(), pageNumber, pageSize)
        {
        }

        public PagedList(IQueryable<T> items, int pageNumber, int pageSize)
        {
            Initialize(items, pageNumber, pageSize);
        }

        protected PagedList()
        {
        }

        protected void Initialize(IQueryable<T> items, int pageNumber, int pageSize)
        {
            TotalItemCount = items.Count();
            var totalPageCount = (int)Math.Ceiling((double)TotalItemCount / pageSize);
            Pager = new Pager(Sanitize(totalPageCount, 1, int.MaxValue), Sanitize(pageNumber, 1, totalPageCount), pageSize);
            var itemsToSkip = CalculateItemsToSkip();
            Items = items.Skip(itemsToSkip).Take(pageSize).ToList();
            ItemCount = Items.Count();
        }

        protected void Initialize<TSource>(IQueryable<TSource> items, Func<TSource, T> map, int pageNumber, int pageSize)
        {
            TotalItemCount = items.Count();
            var totalPageCount = (int)Math.Ceiling((double)TotalItemCount / pageSize);
            Pager = new Pager(Sanitize(totalPageCount, 1, int.MaxValue), Sanitize(pageNumber, 1, totalPageCount), pageSize);
            var itemsToSkip = CalculateItemsToSkip();
            Items = items.Skip(itemsToSkip).Take(pageSize).Select(map).ToList();
            ItemCount = Items.Count();
        }

        public static PagedList<TOut> Create<TIn, TOut>(IQueryable<TIn> query, int pageNumber, int pageSize, Func<TIn, TOut> map)
        {
            var list = new PagedList<TOut>();
            list.Initialize(query, map, pageNumber, pageSize);
            return list;
        }

        private static int Sanitize(int value, int minValue, int maxValue)
        {
            if (value > maxValue)
            {
                value = maxValue;
            }
            if (value < minValue)
            {
                value = minValue;
            }
            return value;
        }

        private int CalculateItemsToSkip()
        {
            var zeroBasedPageNumber = Pager.PageNumber <= 0 ? 0 : Pager.PageNumber - 1;
            var skip = zeroBasedPageNumber * Pager.PageSize;
            if (skip >= TotalItemCount)
            {
                skip = TotalItemCount >= Pager.PageSize ? (TotalItemCount - Pager.PageSize) : 0;
            }
            return skip;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static PagedList<TSomething> Empty<TSomething>()
        {
            return new PagedList<TSomething>(Enumerable.Empty<TSomething>(), 1, 20);
        }
    }
}