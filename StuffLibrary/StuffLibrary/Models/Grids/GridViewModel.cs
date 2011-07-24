using System.Collections.Generic;
using System.Linq;

namespace StuffLibrary.Models.Grids
{
    public class GridViewModel
    {
        public GridViewModel(IEnumerable<IGridRowViewModel> items)
        {
            rows = Parse(items);
            total = items.Count();
            page = 1;
            records = total;
        }

        private static IEnumerable<JqGridRow> Parse(IEnumerable<IGridRowViewModel> items)
        {
            return from item in items select new JqGridRow(item.RowId, item);
        }


        // ReSharper disable InconsistentNaming
        public int total { get; private set; }
        public int page { get; private set; }
        public int records { get; private set; }
        public IEnumerable<JqGridRow> rows { get; private set; }
        public object userdata { get; set; }
        // ReSharper restore InconsistentNaming
    }
}