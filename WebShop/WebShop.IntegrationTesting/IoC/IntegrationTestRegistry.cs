using StructureMap.Configuration.DSL;
using WebShop.Core.Users;
using WebShop.IntegrationTesting.Fakes;
using WebShop.Web.IoC;

namespace WebShop.IntegrationTesting.IoC
{
    public class IntegrationTestRegistry : Registry
    {
        public IntegrationTestRegistry()
        {
            For<IUserSession>().LifecycleIs(Lifecycle.Current).Use<FakeUserSession>();
        }
    }
}