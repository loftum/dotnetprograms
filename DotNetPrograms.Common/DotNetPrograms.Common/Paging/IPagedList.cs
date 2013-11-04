using System.Collections.Generic;

namespace DotNetPrograms.Common.Paging
{
    public interface IPagedList<out T> : IEnumerable<T>
    {
        IEnumerable<T> Items { get; }
        int ItemCount { get; }
        int TotalItemCount { get; }
        Pager Pager { get; }
    }
}