using System;
using System.Collections.Generic;
using System.Linq;

namespace VisualFarmStudio.Common.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static void Each<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                return;
            }
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }
    }
}