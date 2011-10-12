using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Connections;
using DbToolGui.Communication;
using DbToolGui.Providers;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAssemblyLoader>().To<AssemblyLoader>().InSingletonScope();
            Bind<IConnectionFactory>().To<ConnectionFactory>().InSingletonScope();
            Bind<IConnectionProvider>().To<ConnectionProvider>().InSingletonScope();
            Bind<IDatabaseCommunicator>().To<DatabaseCommunicator>().InSingletonScope();
        }
    }
}