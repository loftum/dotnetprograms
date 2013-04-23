using System.Linq;
using BasicManifest.Common.Exceptions;
using BasicManifest.Core.Domain;

namespace BasicManifest.Data.Repositories
{
    public class BMRepo : IBMRepo
    {
        private readonly IDatabaseSession _session;

        public BMRepo(IDatabaseSession session)
        {
            _session = session;
        }

        public T Get<T>(long id) where T : DomainObject
        {
            return _session.Get<T>(id);
        }

        public T GetOrThrow<T>(long id) where T : DomainObject
        {
            var item = Get<T>(id);
            if (item == null)
            {
                throw BMException.Unknown<T>();
            }
            return item;
        }

        public IQueryable<T> GetAll<T>() where T : DomainObject
        {
            return _session.GetAll<T>();
        }

        public T Delete<T>(T item) where T : DomainObject
        {
            return (T) _session.Delete(item);
        }

        public void SaveChanges()
        {
            _session.Flush();
        }

        public T Save<T>(T item) where T : DomainObject
        {
            return (T) _session.Save(item);
        }
    }
}