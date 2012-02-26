using System.Linq;
using NHibernate;
using NHibernate.Linq;
using StuffLibrary.Domain;

namespace StuffLibrary.Repository
{
    public class StuffLibraryRepo : IStuffLibraryRepo
    {
        public ISession Session { get; private set; }

        public StuffLibraryRepo(ISession session)
        {
            Session = session;
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
    }
}