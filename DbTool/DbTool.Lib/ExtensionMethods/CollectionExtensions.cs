using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DbTool.Lib.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static T[] AsArray<T>(this T item)
        {
            item.ShouldNotBeNull("item");
            return new[]{item};
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            collection.ShouldNotBeNull("collection");
            return collection.Where(item => !predicate(item));
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> collection, T item)
        {
            collection.ShouldNotBeNull("collection");
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
            collection.ShouldNotBeNull("collection");
            return !collection.Cast<object>().Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.IsEmpty();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            collection.ShouldNotBeNull("collection");
            return !collection.Any();
        }

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.ShouldNotBeNull("collection");
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
            item.ShouldNotBeNull("item");
            var allItems = new List<T> { item };
            allItems.AddRange(others);
            return allItems;
        }

        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            collection.ShouldNotBeNull("collection");
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static Type GetTypeOfValues(this IEnumerable collection)
        {
            collection.ShouldNotBeNull("collection");
            if (collection.IsEmpty())
            {
                return typeof (object);
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