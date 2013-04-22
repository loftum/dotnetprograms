using BasicManifest.Data.Setup;
using NHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace BasicManifest.Web.Ioc
{
    public class BMRegistry : Registry
    {
        public BMRegistry()
        {
            Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.Assembly("BasicManifest.Common");
                    s.Assembly("BasicManifest.Core");
                    s.Assembly("BasicManifest.Data");
                    s.Assembly("BasicManifest.Lib");
                    s.WithDefaultConventions();
                });
            For<ISessionProvider>().LifecycleIs(Lifecycle.Current).Use<SessionProvider>();
            For<ISession>().LifecycleIs(Lifecycle.Current).Use(GetSession);
        }

        private static ISession GetSession(IContext ctx)
        {
            return ctx.GetInstance<ISessionProvider>().GetSession();
        }
    }
}