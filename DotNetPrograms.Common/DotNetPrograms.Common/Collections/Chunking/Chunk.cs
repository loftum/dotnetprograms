using System.Collections;
using System.Collections.Generic;

namespace DotNetPrograms.Common.Collections.Chunking
{
    public class Chunk<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _items;
        public int Number { get; private set; }
        public bool IsEnumerated { get; private set; }

        public Chunk(IEnumerable<T> items, int chunkNumber)
        {
            _items = items;
            Number = chunkNumber;
        }

        public IEnumerator<T> GetEnumerator()
        {
            IsEnumerated = true;
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}