using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DbTool.Lib.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
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
            item.ShouldNotBeNull("item");
            var allItems = new List<T> {item};
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
    }
}