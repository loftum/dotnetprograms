using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetPrograms.Common.Collections.Chunking
{
    public class ListChunkEnumerator<T> : IEnumerator<Chunk<T>>
    {
        private readonly List<T> _theWholeShebang;
        private readonly int _chunkSize;
        private int _currentIndex;
        private readonly int _lastIndex;
        private int _currentChunkNumber;
        private readonly object _lock = new object();

        public ListChunkEnumerator(List<T> theWholeShebang, int chunkSize)
        {
            _theWholeShebang = theWholeShebang;
            _chunkSize = chunkSize;
            _lastIndex = _theWholeShebang.Count;
        }

        private IEnumerable<T> NextItems(int currentIndex)
        {
            var index = currentIndex;
            var end = Math.Min(_lastIndex, index + _chunkSize);
            do
            {
                yield return _theWholeShebang[index];
                index++;
            }
            while (index < end);
        }

        public bool MoveNext()
        {
            lock(_lock)
            {
                if (_currentIndex >= _lastIndex)
                {
                    return false;
                }
                Current = new Chunk<T>(NextItems(_currentIndex), _currentChunkNumber++);
                _currentIndex += _chunkSize;
                return true;
            }
        }

        public void Reset()
        {
        }

        public Chunk<T> Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
        }
    }
}