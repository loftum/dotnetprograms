using Ninject.Modules;
using VisualFarmStudio.Lib.Interactive;

namespace VisualFarmStudio.NinjectModules
{
    public class InteractiveModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICSharpExecutor>().To<MonoCSharpExecutor>().InSingletonScope();
        }
    }
}