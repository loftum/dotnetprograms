using EnvironmentViewer.Lib.Data;
using EnvironmentViewer.Lib.SessionFactories;
using Ninject.Modules;

namespace EnvironmentViewer.Lib.Modules
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVersionSessionFactoryProvider>().To<VersionSessionFactoryProvider>().InSingletonScope();
            Bind<IVersionRepoProvider>().To<VersionRepoProvider>().InSingletonScope();
        }
    }
}