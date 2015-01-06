using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Wordbank.Lib.Domain;

namespace Wordbank.Lib.Data
{
    public class WordBankRepository : IWordBankRepository
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ISession _session;

        public WordBankRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();
        }

        public void Save<T>(T item) where T : DomainObject
        {
            _session.Save(item);
        }

        public T Get<T>(long id) where T : DomainObject
        {
            return _session.Get<T>(id);
        }
        
        public IQueryable<T> GetAll<T>() where T : DomainObject
        {
            return _session.Query<T>();
        }
    }
}