using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using StuffLibrary.Repository;
using StuffLibrary.Repository.Configuration;

namespace StuffLibrary.IntegrationTesting.NinjectModules
{
    public class RepoSingletonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IChangeStampUpdater>().To<ChangeStampUpdater>().InSingletonScope();
            Bind<IRepositoryConfiguration>().To<RepositoryConfiguration>().InSingletonScope();
            Bind<ISession>().ToMethod(GetSession).InSingletonScope();
            Bind<IStuffLibraryRepo>().To<StuffLibraryRepo>().InSingletonScope();
        }

        private static ISession GetSession(IContext context)
        {
            return context.Kernel.Get<IRepositoryConfiguration>().CreateSessionFactory().OpenSession();
        }
    }
}