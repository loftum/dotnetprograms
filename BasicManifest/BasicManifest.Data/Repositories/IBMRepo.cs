using System;
using System.Linq;
using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Repositories
{
    public interface IBMRepo
    {
        T Get<T>(Guid id) where T : DomainObject;
        IQueryable<T> GetAll<T>() where T : DomainObject;
        T Delete<T>(T item) where T : DomainObject;
        void SaveChanges();
        T GetOrThrow<T>(Guid id) where T : DomainObject;
    }
}