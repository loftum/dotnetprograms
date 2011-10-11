using DbTool.Lib.AssemblyLoading;
using DbTool.Lib.Tasks;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class TaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAssemblyLoader>().To<AssemblyLoader>().InSingletonScope();
            Bind<ITaskFactory>().To<TaskFactory>().InSingletonScope();
        }
    }
}