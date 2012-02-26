using Ninject.Modules;
using StuffLibrary.Common.Scoping;
using StuffLibrary.Lib.UnitOfWork;

namespace StuffLibrary.NinjectModules
{
    public class UnitOfWorkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<NHibernateUnitOfWork>().InCurrentInjectionScope();
        }
    }
}