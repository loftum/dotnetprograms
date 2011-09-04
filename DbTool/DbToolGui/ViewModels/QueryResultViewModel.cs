using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using DbToolGui.Connections;
using DbToolGui.Controls;
using DbToolGui.ExtensionMethods;

namespace DbToolGui.ViewModels
{
    public class QueryResultViewModel
    {
        public ObservableCollection<DataGridColumn> Columns { get; private set; }
        public ObservableCollection<Record> Records { get; private set; }

        public QueryResultViewModel()
        {
            Columns = new ObservableCollection<DataGridColumn>();
            Records = new ObservableCollection<Record>();
        }

        public void Show(QueryResult queryResult)
        {
            foreach (var column in queryResult.Columns)
            {
                var binding = new Binding(string.Format("Properties[{0}]", column.Index)) { Mode = BindingMode.OneWay };
                Columns.Add(new CustomBoundColumn
                {
                    Header = column.Name,
                    Binding = binding,
                    TemplateName = "PropertyValueTemplate"
                });
            }

            Records.AddRange(queryResult.Rows);
        }

        public void Clear()
        {
            Columns.Clear();
            Records.Clear();
        }
    }
}