using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace StuffLibrary.UnitTesting.Asserting
{
    public class CollectionAsserter<T>
    {
        private readonly IEnumerable<T> _items;

        public CollectionAsserter(IEnumerable<T> items)
        {
            _items = items;
        }

        public CollectionAsserter<T> HasCount(int expectedCount)
        {
            AssertCountIs(expectedCount);
            return this;
        }

        private void AssertCountIs(int expectedCount)
        {
            Assert.That(_items.Count(), Is.EqualTo(expectedCount));
        }

        public CollectionAsserter<T> Contains(T expectedItem)
        {
            AssertContains(expectedItem);
            return this;
        }

        private void AssertContains(T expectedItem)
        {
            Assert.That(_items.Contains(expectedItem), string.Format("Collection should contain {0}", expectedItem));
        }
    }
}