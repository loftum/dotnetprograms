using System.Web;
using VisualFarmStudio.Lib.Interactive;
using VisualFarmStudio.Lib.Scoping;
using VisualFarmStudio.NinjectModules;

[assembly: WebActivator.PreApplicationStartMethod(typeof(VisualFarmStudio.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(VisualFarmStudio.App_Start.NinjectMVC3), "Stop")]

namespace VisualFarmStudio.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            InteractiveStuff.Kernel = kernel;
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            InjectionScope.SetScope(() => HttpContext.Current);
            kernel.Load(new RepoModule(), new FacadeModule(), new InteractiveModule());
        }
    }
}
