using StructureMap.Configuration.DSL;
using StructureMap.Graph;

namespace Demo.Web.Ioc
{
    public class DemoWebRegistry : Registry
    {
        public DemoWebRegistry()
        {
            Scan(a =>
            {
                a.TheCallingAssembly();
                a.Assembly("Demo.Core");
                a.WithDefaultConventions().OnAddedPluginTypes(t => t.LifecycleIs(Lifecycle.Current));
            });
        }
    }
}