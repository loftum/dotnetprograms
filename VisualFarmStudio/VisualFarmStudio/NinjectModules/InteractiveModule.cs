using Ninject.Modules;
using VisualFarmStudio.Common.Scoping;
using VisualFarmStudio.Lib.Interactive;

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