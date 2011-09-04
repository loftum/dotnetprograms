using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DbToolGui.ExtensionMethods
{
    public static class DataGridExtensions
    {
        public static ObservableCollection<DataGridColumn> GetColumns(DependencyObject obj)
        {
            return (ObservableCollection<DataGridColumn>)obj.GetValue(ColumnsProperty);
        }

        public static void SetColumns(DependencyObject obj, ObservableCollection<DataGridColumn> value)
        {
            obj.SetValue(ColumnsProperty, value);
        }

        public static readonly DependencyProperty ColumnsProperty = DependencyProperty
            .RegisterAttached("Columns",
                            typeof(ObservableCollection<DataGridColumn>),
                            typeof(DataGridExtensions),
                            new UIPropertyMetadata
                                (new ObservableCollection<DataGridColumn>(),
                                OnDataGridColumnsPropertyChanged));


        private static void OnDataGridColumnsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DataGrid)
            {
                var dataGrid = (DataGrid) d;
                var columns = (ObservableCollection<DataGridColumn>)e.NewValue;

                if (columns != null)
                {
                    dataGrid.Columns.Clear();

                    foreach (var column in columns)
                    {
                        dataGrid.Columns.Add(column);
                    }

                    columns.CollectionChanged += delegate(object sender, NotifyCollectionChangedEventArgs args)
                    {
                        if (args.Action == NotifyCollectionChangedAction.Reset)
                        {
                            dataGrid.Columns.Clear();
                        }
                        if (args.NewItems != null)
                        {
                            foreach (var column in args.NewItems.Cast<DataGridColumn>())
                            {
                                dataGrid.Columns.Add(column);
                            }
                        }

                        if (args.OldItems != null)
                        {
                            foreach (var column in args.OldItems.Cast<DataGridColumn>())
                            {
                                dataGrid.Columns.Remove(column);
                            }
                        }
                    };
                }
            }
        }
    }
}