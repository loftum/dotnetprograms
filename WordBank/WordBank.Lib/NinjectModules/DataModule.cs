using NHibernate;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using WordBank.Lib.Tasks;
using Wordbank.Lib.Config;
using Wordbank.Lib.Data;
using Wordbank.Lib.Logging;

namespace Wordbank.Lib.NinjectModules
{
    public class DataModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaskFactory>().To<TaskFactory>().InSingletonScope();
            Bind<IWordBankParser>().To<WordBankParser>().InSingletonScope();
            Bind<IWordBankSettings>().To<WordBankSettings>().InSingletonScope();
            Bind<IWordBankLogger>().To<WordBankConsoleLogger>().InSingletonScope();
            Bind<ISessionFactoryProvider>().To<SessionFactoryProvider>().InSingletonScope();
            Bind<ISessionFactory>().ToMethod(GetSessionFactory).InSingletonScope();
            Bind<IWordBankRepository>().To<WordBankRepository>().InSingletonScope();
        }

        private static ISessionFactory GetSessionFactory(IContext context)
        {
            return context.Kernel.Get<ISessionFactoryProvider>().CreateSessionFactory();
        }
    }
}