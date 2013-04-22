using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using BasicManifest.Data.Migrating;
using BasicManifest.Lib.Mappings;
using BasicManifest.Web.Ioc;
using StructureMap.Pipeline;

namespace BasicManifest.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Lifecycle.Current = new HttpContextLifecycle();
            ObjectContainer.Init(new BMRegistry());
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            MappingConfiguration.Init(new BasicManifestMappingProfile());
            ObjectContainer.Get<IBasicManifestMigrator>().MigrateUp();
        }
    }
}