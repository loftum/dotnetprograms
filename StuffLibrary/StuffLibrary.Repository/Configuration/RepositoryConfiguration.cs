using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Event;
using StuffLibrary.Common.Configuration;

namespace StuffLibrary.Repository.Configuration
{
    public class RepositoryConfiguration : IRepositoryConfiguration
    {
        private readonly IStuffLibraryConfig _config;
        private readonly IChangeStampUpdater _changeStampUpdater;

        public RepositoryConfiguration(IStuffLibraryConfig config, IChangeStampUpdater changeStampUpdater)
        {
            _config = config;
            _changeStampUpdater = changeStampUpdater;
        }

        public ISessionFactory CreateSessionFactory()
        {
            var factory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(_config.ConnectionString))
                .ExposeConfiguration(RegisterListeners)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RepositoryConfiguration>())
                .BuildSessionFactory();
            return factory;
        }

        private void RegisterListeners(NHibernate.Cfg.Configuration configuration)
        {
            configuration.SetListener(ListenerType.PreInsert, _changeStampUpdater);
            configuration.SetListener(ListenerType.PreUpdate, _changeStampUpdater);
        }
    }
}