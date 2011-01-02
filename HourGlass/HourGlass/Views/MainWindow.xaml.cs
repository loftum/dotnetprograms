using HourGlass.Lib.Modules;
using HourGlass.Modules;
using HourGlass.ViewModels;
using Ninject;

namespace HourGlass.Views
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var kernel = new StandardKernel(new RepoModule(), new ServiceModule(), new HourGlassModule());
            DataContext = kernel.Get<IWeeksViewModel>();
        }
    }
}
