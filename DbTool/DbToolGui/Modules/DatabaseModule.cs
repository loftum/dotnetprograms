using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Communication;
using DbTool.Lib.Connections;
using DbTool.Lib.FileSystem;
using Ninject.Modules;

namespace DbToolGui.Modules
{
    public class DatabaseModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPathResolver>().To<PathResolver>().InSingletonScope();
            Bind<IAssemblyLoader>().To<AssemblyLoader>().InSingletonScope();
            Bind<IDbContextFactory>().To<DbContextFactory>().InSingletonScope();
            Bind<IDatabaseCommunicator>().To<DatabaseCommunicator>().InSingletonScope();
        }
    }
}