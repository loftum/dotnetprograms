using System.Collections.Generic;
using NHibernate;
using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.UnitTesting.Faking
{
    public class FakeQueryOver<T> : IQueryOver<T>
    {
        private readonly IList<T> _items = new List<T>();

        public FakeQueryOver(IEnumerable<T> items)
        {
            _items.AddRange(items);
        }

        public FakeQueryOver(params T[] items)
        {
            _items.AddRange(items);
        }

        public ICriteria UnderlyingCriteria
        {
            get { throw new System.NotImplementedException(); }
        }

        public ICriteria RootCriteria
        {
            get { throw new System.NotImplementedException(); }
        }

        public IList<T> List()
        {
            return _items;
        }

        public IList<U> List<U>()
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T, T> ToRowCountQuery()
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T, T> ToRowCountInt64Query()
        {
            throw new System.NotImplementedException();
        }

        public int RowCount()
        {
            throw new System.NotImplementedException();
        }

        public long RowCountInt64()
        {
            throw new System.NotImplementedException();
        }

        public T SingleOrDefault()
        {
            throw new System.NotImplementedException();
        }

        public U SingleOrDefault<U>()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> Future()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<U> Future<U>()
        {
            throw new System.NotImplementedException();
        }

        public IFutureValue<T> FutureValue()
        {
            throw new System.NotImplementedException();
        }

        public IFutureValue<U> FutureValue<U>()
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T, T> Clone()
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T> ClearOrders()
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T> Skip(int firstResult)
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T> Take(int maxResults)
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T> Cacheable()
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T> CacheMode(CacheMode cacheMode)
        {
            throw new System.NotImplementedException();
        }

        public IQueryOver<T> CacheRegion(string cacheRegion)
        {
            throw new System.NotImplementedException();
        }
    }
}