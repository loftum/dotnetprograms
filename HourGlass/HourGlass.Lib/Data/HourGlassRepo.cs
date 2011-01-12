using System.Collections.Generic;
using HourGlass.Lib.DateAndTime;
using HourGlass.Lib.Domain;
using NHibernate;

namespace HourGlass.Lib.Data
{
    public class HourGlassRepo : IHourGlassRepo
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ISession _session;
        private readonly IDateProvider _dateProvider;

        public HourGlassRepo(ISessionFactory sessionFactory, IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
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
                SetDatesOn(item);
                _session.SaveOrUpdate(item);
                transaction.Commit();
            }
            return item;
        }

        private void SetDatesOn(DomainObject item)
        {
            var now = _dateProvider.Now();
            if (!item.IsPersisted)
            {
                item.CreatedDate = now;
            }
            item.UpdatedDate = now;
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

        public void Dispose()
        {
            if (_session != null)
            {
                if (_session.IsConnected)
                {
                    _session.Disconnect();
                }
                if (_session.IsOpen)
                {
                    _session.Close();
                }
                _session.Dispose();
            }

            if (_sessionFactory != null)
            {
                if (!_sessionFactory.IsClosed)
                {
                    _sessionFactory.Close();
                }
                _sessionFactory.Dispose();
            }
        }
    }
}