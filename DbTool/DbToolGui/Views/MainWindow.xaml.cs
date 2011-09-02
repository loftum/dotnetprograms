using System.Windows;
using DbToolGui.ViewModels;

namespace DbToolGui.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new DbToolGuiViewModel();
        }
    }
}
