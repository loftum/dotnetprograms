using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DbTool.Testing.Assertions
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

        public CollectionAsserter<T> ContainsOnly(params T[] expectedItems)
        {
            AssertCountIs(expectedItems.Length);
            foreach (var expectedItem in expectedItems)
            {
                Assert.That(_items.Contains(expectedItem));
            }
            return this;
        }

        private void AssertCountIs(int expectedCount)
        {
            Assert.That(_items.Count(), Is.EqualTo(expectedCount));
        }
    }
}