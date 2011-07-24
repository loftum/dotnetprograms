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
            Bind<IChangeStampUpdater>().To<ChangeStampUpdater>().InRetainableRequestScope();
            Bind<IRepositoryConfiguration>().To<RepositoryConfiguration>().InRetainableRequestScope();
            Bind<ISession>().ToMethod(GetSession).InRetainableRequestScope();
            Bind<IStuffLibraryRepo>().To<StuffLibraryRepo>().InRetainableRequestScope();
        }

        private static ISession GetSession(IContext context)
        {
            return context.Kernel.Get<IRepositoryConfiguration>().CreateSessionFactory().OpenSession();
        }
    }
}