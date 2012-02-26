using Ninject;
using Ninject.Modules;
using StuffLibrary.Common.Scoping;
using StuffLibrary.IntegrationTesting.UnitOfWork;
using StuffLibrary.Lib.UnitOfWork;

namespace StuffLibrary.IntegrationTesting.NinjectModules
{
    public class UnitOfWorkTestModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<RollbackUnitOfWork>().InCurrentInjectionScope();
            Bind<RollbackUnitOfWork>().ToMethod(c => (RollbackUnitOfWork) c.Kernel.Get<IUnitOfWork>()).InCurrentInjectionScope();
        }
    }
}