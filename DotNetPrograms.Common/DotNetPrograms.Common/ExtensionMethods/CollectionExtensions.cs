using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotNetPrograms.Common.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                list.Add(item);
            }
        }

        public static IEnumerable<IEnumerable<T>> InChunksOf<T>(this IEnumerable<T> collection, int chunkSize)
        {
            var chunk = new List<T>();
            foreach (var item in collection)
            {
                chunk.Add(item);
                if (chunk.Count < chunkSize)
                {
                    continue;
                }
                yield return chunk;
                chunk.Clear();
            }
            if (chunk.Any())
            {
                yield return chunk;
            }
        }

        public static string StringJoin<T>(this IEnumerable<T> collection, string separator, string defaultValue = null)
        {
            return collection.Any() ? string.Join(separator, collection) : defaultValue;
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return collection.Where(item => !predicate(item));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Except(item.AsArray());
        }

        public static void ShouldNotBeNullOrEmpty<T>(this IEnumerable<T> collection, string name)
        {
            if (collection.IsNullOrEmpty())
            {
                throw new ArgumentNullException(string.Format("collection {0} cannot be null or empty", name));
            }
        }

        public static bool IsEmpty(this IEnumerable collection)
        {
            return !collection.Cast<object>().Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.IsEmpty();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return !collection.Any();
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void AddRange<T>(this IList collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static List<T> ToListWith<T>(this T item, params T[] others)
        {
            return item.ToListWith(others.ToList());
        }

        public static List<T> ToListWith<T>(this T item, IEnumerable<T> others)
        {
            var allItems = new List<T> { item };
            allItems.AddRange(others);
            return allItems;
        }

        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static Type GetTypeOfValues(this IEnumerable collection)
        {
            if (collection.IsEmpty())
            {
                return typeof(object);
            }
            Type returnType = null;
            foreach (var item in collection)
            {
                var currentType = item.GetType();
                if (returnType == null)
                {
                    returnType = currentType;
                }
                else if (currentType != returnType)
                {
                    if (returnType.IsAssignableFrom(currentType))
                    {
                        returnType = currentType;
                    }
                    else
                    {
                        return typeof(object);
                    }
                }
            }
            return returnType ?? typeof(object);
        }
    }
}