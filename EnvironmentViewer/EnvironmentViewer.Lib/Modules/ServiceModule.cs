using EnvironmentViewer.Lib.Services;
using Ninject.Modules;

namespace EnvironmentViewer.Lib.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IApplicationService>().To<ApplicationService>().InSingletonScope();
            Bind<IDatabaseService>().To<DatabaseService>().InSingletonScope();
            Bind<IEnvironmentService>().To<EnvironmentService>().InSingletonScope();
        }
    }
}