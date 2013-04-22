using BasicManifest.Common.Configuration;
using BasicManifest.Data.ExtensionMethods;
using BasicManifest.Data.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Diagnostics;
using NHibernate;
using NHibernate.Event;

namespace BasicManifest.Data.Setup
{
    public class SessionFactoryBuilder
    {
        public static ISessionFactory BuildSessionFactory(IAuditEventListener auditEventListener, IBMConfig config)
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                    .ConnectionString(config.BasicManifestConnectionString)
                    .If(config.ShowSql, c => c.ShowSql())
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<PersonMap>()
                    .Conventions.Add(PrimaryKey.Name.Is(e => "Id"))
                    .Conventions.Add(ForeignKey.EndsWith("Id"))
                )
                .Diagnostics(d => SetUpDiagnostics(d, config))
                .ExposeConfiguration(cfg =>
                {
                    cfg.SetListener(ListenerType.PreInsert, auditEventListener);
                    cfg.SetListener(ListenerType.PreUpdate, auditEventListener);
                }
                )
                .BuildSessionFactory();
        }

        private static void SetUpDiagnostics(DiagnosticsConfiguration diagnostics, IBMConfig config)
        {
            diagnostics.Enable(config.EnableNHibernateDiagnostics);
            if (config.ShowSql)
            {
                diagnostics.OutputToConsole();
            }
        }
    }
}