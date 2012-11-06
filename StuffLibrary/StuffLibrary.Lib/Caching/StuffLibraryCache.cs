using System;
using System.Web;
using System.Web.Caching;
using DotNetPrograms.Common.ExtensionMethods;

namespace StuffLibrary.Lib.Caching
{
    public class StuffLibraryCache : IStuffLibraryCache
    {
        private static Cache Cache { get { return HttpRuntime.Cache; } }

        public T Get<T>(Func<T> func, string cacheKey) where T : class
        {
            T value;
            if (TryGet(cacheKey, out value))
            {
                return value;
            }
            value = func();
            Add(cacheKey, value);
            return value;
        }

        private static void Add<T>(string key, T value)
        {
            Cache.Insert(key, value, null, DateTime.MaxValue, 5.Minutes());
        }

        private static bool TryGet<T>(string cacheKey, out T value) where T : class
        {
            value = null;
            try
            {
                value = (T)Cache[cacheKey];
                return value != null;
            }
            catch
            {
                return false;
            }
        }
    }
}