using System.Windows;
using EnvironmentViewer.Lib.Modules;
using EnvironmentViewer.Lib.Services;
using EnvironmentViewer.ViewModels;
using Ninject;

namespace EnvironmentViewer
{
    public partial class MainWindow : Window
    {
        private readonly IKernel _kernel = new StandardKernel(new RepoModule(), new ServiceModule());

        public MainWindow()
        {
            InitializeComponent();
            var environmentService = _kernel.Get<IEnvironmentService>();
            DataContext = new EnvironmentsViewModel(environmentService);
        }
    }
}
