using System.Data;
using MasterData.Core.Data;
using NHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace MasterData.Web.IoC
{
    public class MasterDataRegistry : Registry
    {
        public MasterDataRegistry()
        {
            Scan(s =>
                {
                    s.TheCallingAssembly();
                    s.Assembly("WebShop.Common");
                    s.Assembly("MasterData.Core");
                    s.WithDefaultConventions().OnAddedPluginTypes(a => a.LifecycleIs(Lifecycle.Current));
                });
            For<ISession>().LifecycleIs(Lifecycle.Current).Use(GetSession);
            For<IDbConnection>().LifecycleIs(Lifecycle.Current).Use(GetConnection);
        }

        private static IDbConnection GetConnection(IContext context)
        {
            return GetSession(context).Connection;
        }

        private static ISession GetSession(IContext context)
        {
            return context.GetInstance<ISessionProvider>().CurrentSession;
        }
    }
}