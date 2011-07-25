using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.HtmlTools.Dropdowns
{
    public class SelectableList
    {
        public IEnumerable<SelectListItem> Items { get { return _items; } }
        private readonly IList<SelectListItem> _items;

        public SelectableList(IEnumerable<SelectListItem> items)
        {
            _items = new List<SelectListItem>(items);
        }

        public static SelectableList Of<T>(IEnumerable<T> elements, Func<T,SelectListItem> convert)
        {
            var items = from element in elements select convert(element);
            return new SelectableList(items);
        }

        public static SelectableList OfEnum<T>()
        {
            var values = Enum.GetValues(typeof(T)).Cast<Enum>().ToArray();
            return Of(values);
        }

        public static SelectableList Of(params Enum[] values)
        {
            var items = (from value in values
                         let text = value.GetDescription()
                         let intValue = value.ToIntValue()
                         select new SelectListItem { Text = text, Value = intValue.ToString() });
            return new SelectableList(items);
        }
        
    }
}