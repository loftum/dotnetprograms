using System;
using System.Collections.Generic;
using MovieBase.Domain;
using NHibernate;

namespace MovieBase.Data.Dao
{
    public class MovieBaseRepository : IMovieBaseRepository, IDisposable
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ISession _session;

        public MovieBaseRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();
        }

        ~MovieBaseRepository()
        {
            Dispose();
        }

        public void Dispose()
        {
            _session.Dispose();
            _sessionFactory.Dispose();
        }

        public T Save<T>(T item) where T : DomainObject
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(item);
                transaction.Commit();
            }
            return item;
        }

        public T Get<T>(long id) where T : DomainObject
        {
            return _session.Get<T>(id);
        }

        public IList<T> GetAll<T>() where T : DomainObject
        {
            return _session.CreateCriteria(typeof (T)).List<T>();
        }
    }
}
