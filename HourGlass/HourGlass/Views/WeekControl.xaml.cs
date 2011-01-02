using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using HourGlass.ViewModels;

namespace HourGlass.Views
{
    public partial class WeekControl : UserControl
    {
        public WeekControl()
        {
            InitializeComponent();
            DataContextChanged += HandleContextChanged;
        }

        private void HandleContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var oldViewModel = (WeekViewModel) e.OldValue;
            if (oldViewModel != null)
            {
                oldViewModel.Usages.CollectionChanged -= HandleUsageChange;    
            }
            
            var viewModel = GetModel();
            if (viewModel != null)
            {
                viewModel.Usages.CollectionChanged += HandleUsageChange;
            }

            UpdateUsages();
        }

        private void HandleUsageChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateUsages();
        }

        private WeekViewModel GetModel()
        {
            return (WeekViewModel)DataContext;
        }

        private void UpdateUsages()
        {
            HourUsageGrid.Children.Clear();
            HourUsageGrid.RowDefinitions.Clear();

            var viewModel = GetModel();
            if (viewModel == null)
            {
                return;
            }

            for (var ii = 0; ii < viewModel.Usages.Count; ii++)
            {
                HourUsageGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(24) });
                var usageControl = new HourUsageControl { DataContext = viewModel.Usages[ii] };
                Grid.SetColumn(usageControl, 0);
                Grid.SetRow(usageControl, ii);
                HourUsageGrid.Children.Add(usageControl);
            }
        }
    }
}
