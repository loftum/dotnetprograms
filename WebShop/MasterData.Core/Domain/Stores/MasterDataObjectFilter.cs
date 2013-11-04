using System;
using System.Collections.Generic;
using System.Linq;

namespace MasterData.Core.Domain.Stores
{
    public class MasterDataObjectFilter<T> where T : MasterDataObject
    {
        public virtual bool DefaultInclude { get; set; }
        public virtual IList<Guid> FilterValues { get; set; }

        public MasterDataObjectFilter()
        {
            FilterValues = new List<Guid>();
        }

        public bool Allows(T item)
        {
            return DefaultInclude
                ? !FilterValues.Contains(item.Id)
                : FilterValues.Contains(item.Id);
        }

        public IEnumerable<T> Filter(IEnumerable<T> items)
        {
            return items.Where(Allows);
        }
    }
}