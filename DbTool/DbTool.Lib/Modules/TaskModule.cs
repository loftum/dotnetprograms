using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Connections;
using DbTool.Lib.FileSystem;
using DbTool.Lib.Tasks;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class TaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPathResolver>().To<PathResolver>().InSingletonScope();
            Bind<IAssemblyLoader>().To<AssemblyLoader>().InSingletonScope();
            Bind<IDbContextFactory>().To<DbContextFactory>().InSingletonScope();
            Bind<ITaskFactory>().To<TaskFactory>().InSingletonScope();
        }
    }
}