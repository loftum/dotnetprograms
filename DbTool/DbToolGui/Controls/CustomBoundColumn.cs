using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DbTool.Lib.Data;

namespace DbToolGui.Controls
{
    public class CustomBoundColumn : DataGridBoundColumn
    {
        private readonly ColumnDescriptor _descriptor;

        public CustomBoundColumn(ColumnDescriptor descriptor)
        {
            _descriptor = descriptor;
            Header = _descriptor.Name;
        }

        public void Update(DataGridRow row)
        {
            var record = (Record)row.Item;
            var content = GetContent(row);
            if (content == null)
            {
                return;
            }
            content.Text = record.Properties[_descriptor.Index].ValueAsString;
        }

        private TextBox GetContent(DataGridRow row)
        {
            return (TextBox) GetCellContent(row);
        }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var record = (Record) dataItem;
            return new TextBox
                          {
                              IsReadOnly = true,
                              Height = 20,
                              Text = record.Properties[_descriptor.Index].ValueAsString,
                              DataContext = dataItem,
                              BorderThickness = new Thickness(0)
                          };
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}