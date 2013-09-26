using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using MasterData.Core.Mapping;
using MasterData.Migrations;
using MasterData.Web.IoC;
using StructureMap;
using StructureMap.Pipeline;
using WebShop.Common.Configuration;
using WebShop.Common.Mapping;

namespace MasterData.Web
{
    public class MasterDataApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SetUpDatabase();
            SetUpIoc();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMap.Initialize(new MasterDataMappingProfile());
        }

        private static void SetUpDatabase()
        {
            new MasterDataMigrator(new ConfigSettings().MasterDataConnectionString).MigrateUp();
        }

        private static void SetUpIoc()
        {
            Lifecycle.Current = new HttpContextLifecycle();
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            ObjectFactory.Initialize(a => a.AddRegistry(new MasterDataRegistry()));
        }
    }
}