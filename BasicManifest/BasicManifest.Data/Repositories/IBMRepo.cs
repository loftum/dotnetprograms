using System.Linq;
using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Repositories
{
    public interface IBMRepo
    {
        T Get<T>(long id) where T : DomainObject;
        IQueryable<T> GetAll<T>() where T : DomainObject;
        T Delete<T>(T item) where T : DomainObject;
        void SaveChanges();
        T GetOrThrow<T>(long id) where T : DomainObject;
    }
}