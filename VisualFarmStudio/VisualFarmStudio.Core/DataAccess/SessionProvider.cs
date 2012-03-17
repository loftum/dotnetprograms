using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using VisualFarmStudio.Common.Configuration;
using VisualFarmStudio.Core.Mappings;

namespace VisualFarmStudio.Core.DataAccess
{
    public class SessionProvider : ISessionProvider
    {
        private readonly IVFSConfig _config;
        private readonly ISessionFactory _factory;
        private ISession _session;

        public SessionProvider(IVFSConfig config)
        {
            _config = config;
            _factory = BuildSessionFactory();
        }

        private ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_config.ConnectionString))
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<BondegardMap>()
                    .Conventions.Add(PrimaryKey.Name.Is(e => "Id"))
                    .Conventions.Add(ForeignKey.EndsWith("Id"))
                )
                .BuildSessionFactory();
        }

        public ISession GetSession()
        {
            if (_session == null || !_session.IsOpen)
            {
                _session = _factory.OpenSession();
            }
            return _session;
        }
    }
}