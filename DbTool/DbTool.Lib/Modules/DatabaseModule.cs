using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Communication;
using DbTool.Lib.Connections;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConnectionDataProvider>().To<ConnectionDataProvider>().InSingletonScope();
            Bind<IDatabaseCommunicator>().To<DatabaseCommunicator>().InSingletonScope();
        }
    }
}

