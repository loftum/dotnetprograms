using System;
using System.Collections.Generic;
using HourGlass.Lib.Domain;
using NHibernate;

namespace HourGlass.Lib.Data
{
    public class HourGlassRepo : IHourGlassRepo
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ISession _session;

        public HourGlassRepo(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();
        }

        public T Get<T>(long id) where T : DomainObject
        {
            return _session.Get<T>(id);
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

        public T Delete<T>(T item) where T : DomainObject
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(item);
                transaction.Commit();
            }
            return item;
        }

        public IEnumerable<T> GetAll<T>() where T : DomainObject
        {
            return _session.CreateCriteria(typeof (T)).List<T>();
        }
    }
}