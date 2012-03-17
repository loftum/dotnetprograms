using Ninject.Modules;
using VisualFarmStudio.Lib.Interactive;
using VisualFarmStudio.Lib.Scoping;

namespace VisualFarmStudio.NinjectModules
{
    public class InteractiveModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInteractiveShell>().To<InteractiveShell>().InScope(c => InjectionScope.Current);
            Bind<ICSharpExecutor>().To<MonoCSharpExecutor>().InSingletonScope();
        }
    }
}