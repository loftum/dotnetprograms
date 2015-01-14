using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Convenient.Mvc.Startup))]
namespace Convenient.Mvc
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
