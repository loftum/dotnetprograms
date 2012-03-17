using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using VisualFarmStudio.Common.Scoping;
using VisualFarmStudio.Core.DataAccess;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.Migrating;
using VisualFarmStudio.Lib.UnitOfWork;

namespace VisualFarmStudio.NinjectModules
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVisualFarmMigrator>().To<VisualFarmMigrator>().InScope(c => InjectionScope.Current);
            Bind<IUnitOfWork>().To<NHibernateUnitOfWork>().InScope(c => InjectionScope.Current);
            Bind<ISessionProvider>().To<SessionProvider>().InSingletonScope();
            Bind<ISession>().ToMethod(GetSession).InScope(c => InjectionScope.Current);
            Bind<IVisualFarmRepo>().To<VisualFarmRepo>().InScope(c => InjectionScope.Current);
        }

        private static ISession GetSession(IContext context)
        {
            return context.Kernel.Get<ISessionProvider>().GetSession();
        }
    }
}