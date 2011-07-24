using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StuffLibrary.Attributes;

namespace StuffLibrary.Models.Grids
{
    public class JqGridRow
    {
        // ReSharper disable InconsistentNaming
        public string id { get; private set; }
        public IEnumerable<string> cell { get; private set; } 
        // ReSharper restore InconsistentNaming

        public JqGridRow(string id, object item)
        {
            this.id = id;
            cell = CreateCells(item);
        }

        private static IEnumerable<string> CreateCells(object item)
        {
            var cells = new List<string>();

            foreach (var property in item.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (property.GetCustomAttributes(typeof(TransferToGridAttribute), false).Count() > 0)
                {
                    var propertyValues = property.GetValue(item, null);
                    if (propertyValues is IEnumerable<object>)
                    {
                        cells.AddRange(((IEnumerable<object>)propertyValues).Select(FormatAsString));
                    }
                    else
                    {
                        cells.Add(FormatAsString(propertyValues));
                    }
                }
            }

            return cells;
        }

        private static string FormatAsString(object propertyValue)
        {
            return propertyValue == null ?
                string.Empty :
                propertyValue.ToString();
        }
    }
}