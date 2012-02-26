using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using StuffLibrary.Common.Scoping;
using StuffLibrary.Repository;
using StuffLibrary.Repository.Configuration;

namespace StuffLibrary.NinjectModules
{
    public class RepoModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IChangeStampUpdater>().To<ChangeStampUpdater>().InCurrentInjectionScope();
            Bind<IRepositoryConfiguration>().To<RepositoryConfiguration>().InCurrentInjectionScope();
            Bind<ISessionProvider>().To<SessionProvider>().InCurrentInjectionScope();
            Bind<ISession>().ToMethod(GetSession).InCurrentInjectionScope();
            Bind<IStuffLibraryRepo>().To<StuffLibraryRepo>().InCurrentInjectionScope();
        }

        private static ISession GetSession(IContext context)
        {
            return context.Kernel.Get<ISessionProvider>().GetCurrent();
        }
    }
}