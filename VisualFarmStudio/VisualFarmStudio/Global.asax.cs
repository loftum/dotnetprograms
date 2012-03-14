using System.Web.Mvc;
using System.Web.Routing;
using MigSharp;
using VisualFarmStudio.Lib.Configuration;
using VisualFarmStudio.Lib.ExtensionMethods;
using VisualFarmStudio.Migrations.Steps;

namespace VisualFarmStudio
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            MigrateDatabase();
        }

        private void MigrateDatabase()
        {
            var config = new VFSConfig();
            var options = new MigrationOptions();
            options.SupportedProviders.Set(ProviderNames.SqlServer2008.AsArray());
            var migrator = new Migrator(config.ConnectionString, ProviderNames.SqlServer2008, options);
            migrator.MigrateAll(typeof(V001_Initial_1).Assembly);
        }
    }
}