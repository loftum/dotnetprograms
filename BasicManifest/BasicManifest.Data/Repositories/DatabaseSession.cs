using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace BasicManifest.Data.Repositories
{
    public class DatabaseSession : IDatabaseSession
    {
        private readonly ISession _session;

        public DatabaseSession(ISession session)
        {
            _session = session;
        }

        public T Get<T>(object id)
        {
            return _session.Get<T>(id);
        }

        public IQueryable<T> GetAll<T>()
        {
            return _session.Query<T>();
        }

        public object Delete(object item)
        {
            _session.Delete(item);
            return item;
        }

        public object Save(object item)
        {
            _session.Save(item);
            return item;
        }

        public void Flush()
        {
            _session.Flush();
        }
    }
}