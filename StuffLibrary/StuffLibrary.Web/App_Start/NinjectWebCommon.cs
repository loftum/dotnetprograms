using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using StuffLibrary.Common.DependencyInjection;
using StuffLibrary.Web.NinjectModules;

[assembly: WebActivator.PreApplicationStartMethod(typeof(StuffLibrary.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(StuffLibrary.Web.App_Start.NinjectWebCommon), "Stop")]

namespace StuffLibrary.Web.App_Start
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            InjectionScope.SetScope(() => HttpContext.Current);
            var kernel = new StandardKernel(new LogicModule());

            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
        }        
    }
}
