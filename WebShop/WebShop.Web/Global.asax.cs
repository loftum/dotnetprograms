using System.Data.Entity;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;
using StructureMap.Pipeline;
using WebShop.Common.Configuration;
using WebShop.Common.Mapping;
using WebShop.Core.Data;
using WebShop.Core.Mapping;
using WebShop.Migrations;
using WebShop.Web.IoC;

namespace WebShop.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SetUpDatabase();
            SetUpIoc();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMap.Initialize(new WebShopMappingProfile());
        }

        private static void SetUpDatabase()
        {
            new WebShopMigrator(new ConfigSettings().MasterDataConnectionString).MigrateUp();
            DisableEntityFramworkMigrations();
        }

        private static void DisableEntityFramworkMigrations()
        {
            Database.SetInitializer(new DoNotInitializeDatabase<TransactionsRepository>());
        }

        private static void SetUpIoc()
        {
            Lifecycle.Current = new HttpContextLifecycle();
            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            ObjectFactory.Initialize(a => a.AddRegistry(new WebShopRegistry()));
        }
    }
}