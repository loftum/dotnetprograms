using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Demo.Web.Ioc;
using StructureMap;
using StructureMap.Web.Pipeline;

namespace Demo.Web
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SetUpIoc();
        }

        private static void SetUpIoc()
        {
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            Lifecycle.Current = new HybridLifecycle();
            ObjectFactory.Initialize(a => a.AddRegistry(new DemoWebRegistry()));
        }
    }
}
