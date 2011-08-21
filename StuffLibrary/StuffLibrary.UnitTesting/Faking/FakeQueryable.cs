using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.UnitTesting.Faking
{
    public class FakeQueryable<T> : IQueryable<T>
    {
        private readonly IList<T> _items = new List<T>();

        public FakeQueryable(params T[] items)
        {
            _items.AddRange(items);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Expression Expression
        {
            get
            { throw new NotImplementedException(); }
        }

        public Type ElementType
        {
            get
            { return typeof (T); }
        }

        public IQueryProvider Provider
        {
            get { throw new NotImplementedException(); }
        }
    }
}