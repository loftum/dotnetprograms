using System.Linq;
using NHibernate;
using NHibernate.Linq;
using StuffLibrary.Domain;

namespace StuffLibrary.Repository
{
    public class StuffLibraryRepo : IStuffLibraryRepo
    {
        public ISession Session { get; private set; }
        private readonly ITransaction _transaction;

        public StuffLibraryRepo(ISession session)
        {
            Session = session;
            _transaction = Session.BeginTransaction();
        }

        public TDomainObject Get<TDomainObject>(long id) where TDomainObject : DomainObject
        {
            return Session.Get<TDomainObject>(id);
        }

        public IQueryable<TDomainObject> GetAll<TDomainObject>() where TDomainObject : DomainObject
        {
            return Session.Query<TDomainObject>();
        }

        public void Add<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject
        {
            Session.Save(domainObject);
        }

        public void Delete<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject
        {
            Session.Delete(domainObject);
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
            Session.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}