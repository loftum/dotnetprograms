using DbToolGui.Communication;
using DbToolGui.Providers;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IConnectionProvider>().To<ConnectionProvider>().InSingletonScope();
            Bind<IDatabaseCommunicator>().To<DatabaseCommunicator>().InSingletonScope();
        }
    }
}