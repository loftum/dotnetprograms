using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DbToolGui.Communication.Commands;
using DbToolGui.Controls;
using DbToolGui.Data;
using DbToolGui.ExtensionMethods;

namespace DbToolGui.ViewModels
{
    public class QueryResultViewModel : ViewModelBase
    {
        public Visibility TableVisibility
        {
            get { return Columns.IsNullOrEmpty() ? Visibility.Hidden : Visibility.Visible; }
        }

        public Visibility ResultTextVisibility
        {
            get { return ResultText.IsNullOrEmpty() ? Visibility.Hidden : Visibility.Visible; }
        }

        private string _resultText;
        public string ResultText
        {
            get { return _resultText; }
            set { _resultText = value; OnPropertyChanged("ResultText"); }
        }
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
            ResultText = string.Format("Rowcount: {0}", queryResult.Rowcount);
        }

        public void Show(IDbCommandResult result)
        {
            if (result is QueryResult)
            {
                Show((QueryResult) result);
            }
            else
            {
                Show(result.ToString());    
            }
            FireVisibilityChanged();
        }

        public void Show(string resultText)
        {
            ResultText = resultText;
        }

        public void Clear()
        {
            Columns.Clear();
            Records.Clear();
            FireVisibilityChanged();
            ResultText = string.Empty;
        }

        private void FireVisibilityChanged()
        {
            OnPropertiesChanged("TableVisibility", "ResultTextVisibility");   
        }
    }
}