using DbTool.Lib.Tasks;
using Ninject.Modules;

namespace DbTool.Lib.Modules
{
    public class TaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaskFactory>().To<TaskFactory>().InSingletonScope();
        }
    }
}