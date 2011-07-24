using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Mvc;
using StuffLibrary.Common.Configuration;
using StuffLibrary.Migrations;
using StuffLibrary.NinjectModules;

namespace StuffLibrary
{
    public class MvcApplication : NinjectHttpApplication
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
                new { controller = "Movie", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            MigrateDatabase();
        }

        private static void MigrateDatabase()
        {
            var config = StuffLibraryConfig.Instance;
            new Migrator(config.Databaseprovider, config.ConnectionString).MigrateToLatest();
        }

        protected override void OnApplicationStopped()
        {
            
        }

        protected override IKernel CreateKernel()
        {
            return new StandardKernel(new CommonModule(), new ConfigModule(), new RepoModule(), new LogicModule());
        }
    }
}