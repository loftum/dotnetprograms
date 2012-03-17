using System;
using System.Web;
using System.Web.Caching;

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

        public T Read<T>(string key) where T : class
        {
            try
            {
                return (T)HttpContext.Current.Cache[key];
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool TryRead<T>(string key, out T value) where T : class
        {
            value = Read<T>(key);
            return value != null;
        }

        public void Write(string key, object value)
        {
            if (value == null)
            {
                HttpContext.Current.Cache.Remove(key);
                return;
            }
            HttpContext.Current.Cache.Insert(key, value, null, DateTime.Now.AddMinutes(90), Cache.NoSlidingExpiration);
        }
    }
}