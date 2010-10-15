using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Deploy.Testing.Assertions
{
    public class CollectionAsserter<T>
    {
        private readonly IEnumerable<T> _collection;

        public CollectionAsserter(IEnumerable<T> collection)
        {
            _collection = collection;
        }

        public static CollectionAsserter<T> AssertThat(IEnumerable<T> collection)
        {
            return new CollectionAsserter<T>(collection);
        }

        public CollectionAsserter<T> Contains(T expectedItem)
        {
            Assert.That(_collection.Contains(expectedItem), ContainErrorMessage("Collection should contain", expectedItem));
            return this;
        }

        public CollectionAsserter<T> NotContains(T expectedItem)
        {
            Assert.That(_collection.Contains(expectedItem), Is.False, ContainErrorMessage("Collection should not contain", expectedItem));
            return this;
        }

        private string ContainErrorMessage(string message, T expectedItem)
        {
            return new StringBuilder(message)
                .Append(" '").Append(expectedItem).Append("' ")
                .Append(" but contained ")
                .Append(ElementsInCollection()).ToString();
        }

        private string ElementsInCollection()
        {
            return new StringBuilder("{ ")
                .Append(string.Join(",", _collection))
                .Append(" }").ToString();
        }
    }
}