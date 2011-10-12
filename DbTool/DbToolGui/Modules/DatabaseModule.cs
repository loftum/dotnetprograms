using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Communication;
using DbTool.Lib.Connections;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAssemblyLoader>().To<AssemblyLoader>().InSingletonScope();
            Bind<IDbContextFactory>().To<DbContextFactory>().InSingletonScope();
            Bind<IConnectionDataProvider>().To<ConnectionDataProvider>().InSingletonScope();
            Bind<IDatabaseCommunicator>().To<DatabaseCommunicator>().InSingletonScope();
        }
    }
}