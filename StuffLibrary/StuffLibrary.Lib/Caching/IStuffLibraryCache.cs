using System;

namespace StuffLibrary.Lib.Caching
{
    public interface IStuffLibraryCache
    {
        T Get<T>(Func<T> func, string cacheKey) where T : class;
    }
}