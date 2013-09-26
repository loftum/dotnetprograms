using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Diagnostics;
using MasterData.Core.Domain.Mappings;
using NHibernate;
using WebShop.Common.Configuration;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Data
{
    public class SessionProvider : ISessionProvider
    {
        private static readonly ISessionFactory SessionFactory = BuildSessionFactory(new ConfigSettings());

        private readonly object _lock = new object();
        private ISession _session;
        public ISession CurrentSession
        {
            get
            {
                lock (_lock)
                {
                    if (_session == null || !_session.IsOpen)
                    {
                        _session = CreateNewSession();
                    }
                    return _session;
                }
            }
        }

        private static ISession CreateNewSession()
        {
            return SessionFactory.OpenSession();
        }

        private static ISessionFactory BuildSessionFactory(IConfigSettings settings)
        {
            return Fluently.Configure().Database((IPersistenceConfigurer) MsSqlConfiguration.MsSql2008
                                                                                            .ConnectionString(c => c.FromConnectionStringWithKey("MasterData"))
                                                                                            .If(settings.ShowNhSql, c => c.ShowSql())
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductMasterMap>())
                .Diagnostics(d => SetUpDiagnostics(d, settings))
                .BuildSessionFactory();

        }

        private static void SetUpDiagnostics(DiagnosticsConfiguration d, IConfigSettings settings)
        {
            d.Enable(settings.EnableNhDiagnostics);
            if (settings.EnableNhDiagnostics)
            {
                d.OutputToConsole();
            }
        }
    }
}