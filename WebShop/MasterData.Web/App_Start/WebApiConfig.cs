using System.Web.Http;
using System.Web.Mvc;
using MasterData.Web.IoC;
using StructureMap;

namespace MasterData.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
            config.DependencyResolver = new StructureMapDependencyResolver(ObjectFactory.Container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
