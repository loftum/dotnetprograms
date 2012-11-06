using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPrograms.Common.Paging
{
    public class PagedListModel<T> : IEnumerable<T>
    {
        public IEnumerable<T> Items { get; private set; }
        public int ItemCount { get; private set; }
        public int TotalItemCount { get; private set; }

        public PagerModel Pager { get; private set; }

        public PagedListModel(IEnumerable<T> items, int pageNumber, int pageSize)
            : this(items.AsQueryable(), pageNumber, pageSize)
        {
        }

        public PagedListModel(IQueryable<T> items, int pageNumber, int pageSize)
        {
            TotalItemCount = items.Count();
            var totalPageCount = (int)Math.Ceiling((double)TotalItemCount / pageSize);
            Pager = new PagerModel(Sanitize(totalPageCount, 1, int.MaxValue), Sanitize(pageNumber, 1, totalPageCount), pageSize);
            var itemsToSkip = CalculateItemsToSkip();
            Items = items.Skip(itemsToSkip).Take(pageSize).ToList();
            ItemCount = Items.Count();
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

        public static PagedListModel<TSomething> Empty<TSomething>()
        {
            return new PagedListModel<TSomething>(Enumerable.Empty<TSomething>(), 1, 20);
        }
    }
}