using System.Collections.Generic;

namespace DotNetPrograms.Common.Paging
{
    public class Pager
    {
        public int TotalPageCount { get; private set; }
        public int PageSize { get; private set; }

        public bool HasPreviousPage { get { return PreviousPageNumber < PageNumber; } }
        public int PreviousPageNumber { get { return PageNumber == 1 ? PageNumber : PageNumber - 1; } }
        public int PageNumber { get; private set; }
        public int NextPageNumber { get { return PageNumber == TotalPageCount ? PageNumber : PageNumber + 1; } }
        public bool HasNextPage { get { return NextPageNumber > PageNumber; } }

        public bool IsFirstPage { get { return PageNumber == 1; } }
        public bool IsLastPage { get { return PageNumber == TotalPageCount; } }

        public IEnumerable<int> AvailablePageSizes { get { return new[] { 10, 25, 50, 100 }; } }
        public static int DefaultPageSize { get { return 25; } }

        public Pager(int totalPageCount, int pageNumber, int pageSize)
        {
            TotalPageCount = totalPageCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}