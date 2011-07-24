using NHibernate;
using StuffLibrary.Domain;

namespace StuffLibrary.Repository
{
    public class StuffLibraryRepo : IStuffLibraryRepo
    {
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public StuffLibraryRepo(ISession session)
        {
            _session = session;
            _transaction = _session.BeginTransaction();
        }

        public TDomainObject Get<TDomainObject>(long id) where TDomainObject : DomainObject
        {
            return _session.Get<TDomainObject>(id);
        }

        public IQueryOver<TDomainObject> GetAll<TDomainObject>() where TDomainObject : DomainObject
        {
            return _session.QueryOver<TDomainObject>();
        }

        public void Add<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject
        {
            _session.Save(domainObject);
        }

        public void Delete<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject
        {
            _session.Delete(domainObject);
        }

        public void SaveChanges()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            if (!_transaction.WasCommitted)
            {
                _transaction.Rollback();
            }
            _transaction.Dispose();
            _session.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}