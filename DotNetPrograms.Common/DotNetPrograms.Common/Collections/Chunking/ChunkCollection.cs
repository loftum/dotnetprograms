using System.Collections;
using System.Collections.Generic;

namespace DotNetPrograms.Common.Collections.Chunking
{
    public class ChunkCollection<T> : IEnumerable<Chunk<T>>
    {
        private readonly IEnumerable<T> _theWholeShebang;
        private readonly int _chunkSize;

        public ChunkCollection(IEnumerable<T> theWholeShebang, int chunkSize)
        {
            _theWholeShebang = theWholeShebang;
            _chunkSize = chunkSize;
        }

        public IEnumerator<Chunk<T>> GetEnumerator()
        {
            var list = _theWholeShebang as List<T>;
            if (list == null)
            {
                return new ChunkEnumerator<T>(_theWholeShebang, _chunkSize);
            }
            return new ListChunkEnumerator<T>(list, _chunkSize);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}