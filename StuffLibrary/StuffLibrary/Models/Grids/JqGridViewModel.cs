using System;
using System.Collections.Generic;
using System.Linq;

namespace StuffLibrary.Models.Grids
{
    public class JqGridViewModel
    {
        public JqGridViewModel(IEnumerable<IGridRowViewModel> items)
        {
            rows = Parse(items);
            page = 1;
            total = items.Count();
            records = rows.Count();
        }

        public JqGridViewModel(JqGridParameters parameters, IEnumerable<IGridRowViewModel> items)
        {
            page = parameters.page > 0 ? parameters.page : 1;
            records = items.Count();
            total = (int) Math.Ceiling((double) records / parameters.rows);
            var itemsToSkip = (page - 1) * parameters.rows;
            
            rows =  from item in items
                       .Skip(itemsToSkip)
                       .Take(parameters.rows)
                   select item.ToJqGridRow();
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