using System;
using System.Windows;
using EnvironmentViewer.Lib.Modules;
using EnvironmentViewer.Lib.Services;
using EnvironmentViewer.ViewModels;
using Ninject;

namespace EnvironmentViewer
{
    public partial class App : Application
    {
        protected override void OnActivated(EventArgs e)
        {
            var kernel = new StandardKernel(new RepoModule(), new ServiceModule());
            var environmentService = kernel.Get<IEnvironmentService>();
            MainWindow.DataContext = new EnvironmentsViewModel(environmentService);
        }
    }
}
