using Ninject.Activation;
using Ninject.Modules;
using StuffLibrary.Common.DateAndTime;
using StuffLibrary.Common.Logging;
using StuffLibrary.Common.Scoping;

namespace StuffLibrary.NinjectModules
{
    public class CommonModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDateProvider>().To<DateProvider>().InRetainableRequestScope();
            Bind(typeof (IStuffLibraryLogger)).ToMethod(CreateLogger).InRetainableRequestScope();
        }

        private static object CreateLogger(IContext context)
        {
            var name = context.Request.ParentRequest.Service.Name;
            return new StuffLibraryLogger(name);
        }
    }
}