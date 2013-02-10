using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DotNetPrograms.Common.Collections.Chunking
{
    public class ChunkEnumerator<T> : IEnumerator<Chunk<T>>
    {
        private readonly IEnumerable<T> _theWholeShebang;
        private readonly IEnumerator<T> _enumerator;
        private readonly int _chunkSize;
        private int _currentIndex;
        private int _currentChunkNumber;

        public T LastEnumeratedValue { get { return _enumerator.Current; } }

        public Chunk<T> Current { get; private set; }

        public ChunkEnumerator(IEnumerable<T> theWholeShebang, int chunkSize)
        {
            _theWholeShebang = theWholeShebang;
            _enumerator = _theWholeShebang.GetEnumerator();
            _chunkSize = chunkSize;
        }

        private IEnumerable<T> NextItems()
        {
            var maxIndex = _currentIndex + _chunkSize;
            do
            {
                yield return _enumerator.Current;
                _currentIndex++;
            }
            while (_currentIndex < maxIndex && _enumerator.MoveNext());
        }

        public bool MoveNext()
        {
            if (!_enumerator.MoveNext())
            {
                return false;
            }
            Current = new Chunk<T>(NextItems().ToList(), _currentChunkNumber++);
            return true;
        }

        public void Reset()
        {
            _enumerator.Reset();
            _currentIndex = 0;
            Current = null;
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }
    }
}