using System;
using System.Windows;
using EnvironmentViewer.ViewModels;

namespace EnvironmentViewer
{
    public partial class App : Application
    {
        protected override void OnActivated(EventArgs e)
        {
            MainWindow.DataContext = new EnvironmentsViewModel();
        }
    }
}
