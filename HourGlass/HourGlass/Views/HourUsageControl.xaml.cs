using System.Windows.Controls;
using System.Windows.Input;
using HourGlass.ViewModels;

namespace HourGlass.Views
{
    public partial class HourUsageControl : UserControl
    {
        public HourUsageControl()
        {
            InitializeComponent();
        }

        private void HourCodeBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                var viewModel = (HourUsageViewModel) DataContext;
                if (viewModel == null)
                {
                    return;
                }
                viewModel.AddHourCodeCommand.Execute(HourCodeBox.Text);
            }
        }
    }
}
