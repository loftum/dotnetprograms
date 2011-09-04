using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DbToolGui.Controls
{
    public class CustomBoundColumn : DataGridBoundColumn
    {
        public string TemplateName { get; set; }

        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            var binding = new Binding(((Binding) Binding).Path.Path)
                {
                    Source = dataItem
                };
            var content = new ContentControl
                {
                    ContentTemplate = (DataTemplate) cell.FindResource(TemplateName)
                };
            content.SetBinding(ContentControl.ContentProperty, binding);
            return content;
        }

        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            return GenerateElement(cell, dataItem);
        }
    }
}