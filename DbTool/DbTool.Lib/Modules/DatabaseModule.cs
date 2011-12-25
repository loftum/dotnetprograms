using DbTool.Lib.Communication;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDatabaseCommunicator>().To<DatabaseCommunicator>().InSingletonScope();
        }
    }
}

