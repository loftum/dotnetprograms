using System;

namespace VisualFarmStudio.Lib.Caching
{
    public interface ICache
    {
        T Read<T>(string key) where T : class;
        T Read<T>(string key, Func<T> cacheMissFunc) where T : class;
        void Write(string key, object value);
    }
}