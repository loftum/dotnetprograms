using System;
using System.Web;

namespace VisualFarmStudio.Lib.Caching
{
    public class HttpCache : ICache
    {
        public T Read<T>(string key, Func<T> cacheMissFunc) where T : class
        {
            T value;
            if (TryRead(key, out value))
            {
                return value;
            }

            value = cacheMissFunc();
            Write(key, value);
            
            return value;
        }

        private static bool TryRead<T>(string key, out T value) where T : class
        {
            value = null;
            try
            {
                value = (T)HttpContext.Current.Cache[key];
                return value != null;
            }
            catch
            {
                return false;
            }
        }

        public void Write(string key, object value)
        {
            HttpContext.Current.Cache[key] = value;
        }
    }
}