using System;
using System.Linq;
using NHibernate;
using StuffLibrary.Domain;

namespace StuffLibrary.Repository
{
    public interface IStuffLibraryRepo : IDisposable
    {
        ISession Session { get; }
        TDomainObject Get<TDomainObject>(long id) where TDomainObject : DomainObject;
        IQueryable<TDomainObject> GetAll<TDomainObject>() where TDomainObject : DomainObject;

        void Add<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject;
        void Delete<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject;
        void SaveChanges();
        void Rollback();
    }
}