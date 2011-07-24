using System;
using NHibernate;
using StuffLibrary.Domain;

namespace StuffLibrary.Repository
{
    public interface IStuffLibraryRepo : IDisposable
    {
        TDomainObject Get<TDomainObject>(long id) where TDomainObject : DomainObject;
        IQueryOver<TDomainObject> GetAll<TDomainObject>() where TDomainObject : DomainObject;
        void Add<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject;
        void Delete<TDomainObject>(TDomainObject domainObject) where TDomainObject : DomainObject;
        void SaveChanges();
        void Rollback();
    }
}