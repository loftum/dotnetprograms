using Ninject.Modules;
using StuffLibrary.Common.DateAndTime;

namespace StuffLibrary.IntegrationTesting.NinjectModules
{
    public class CommonSingletonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDateProvider>().To<DateProvider>().InSingletonScope();
        }
    }
}