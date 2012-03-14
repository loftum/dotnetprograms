using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using VisualFarmStudio.Lib.Configuration;
using VisualFarmStudio.Lib.Mappings;

namespace VisualFarmStudio.Lib.DataAccess
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
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<BondegardMap>())
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