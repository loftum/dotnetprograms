using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Connections;
using DbTool.Lib.Tasks;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class TaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAssemblyLoader>().To<AssemblyLoader>().InSingletonScope();
            Bind<IDbContextFactory>().To<DbContextFactory>().InSingletonScope();
            Bind<ITaskFactory>().To<TaskFactory>().InSingletonScope();
        }
    }
}