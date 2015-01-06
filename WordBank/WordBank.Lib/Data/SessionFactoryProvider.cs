using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using WordBank.Lib.DomainMapping;
using Wordbank.Lib.Config;

namespace Wordbank.Lib.Data
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private readonly IWordBankSettings _settings;

        public SessionFactoryProvider(IWordBankSettings settings)
        {
            _settings = settings;
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                  MsSqlConfiguration.MsSql2008.ConnectionString(_settings.ConnectionString)
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrdMap>())
                .BuildSessionFactory();
        }
    }
}