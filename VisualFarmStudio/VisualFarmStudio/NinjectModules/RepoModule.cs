using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using VisualFarmStudio.Core.Repository;
using VisualFarmStudio.Lib.DataAccess;
using VisualFarmStudio.Lib.Scoping;
using VisualFarmStudio.Lib.UnitOfWork;

namespace VisualFarmStudio.NinjectModules
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<NHibernateUnitOfWork>().InScope(c => InjectionScope.Current);
            Bind<ISessionProvider>().To<SessionProvider>().InScope(c => InjectionScope.Current);
            Bind<ISession>().ToMethod(GetSession).InScope(c => InjectionScope.Current);
            Bind<IVisualFarmRepo>().To<VisualFarmRepo>().InScope(c => InjectionScope.Current);
        }

        private static ISession GetSession(IContext context)
        {
            return context.Kernel.Get<ISessionProvider>().GetSession();
        }
    }
}