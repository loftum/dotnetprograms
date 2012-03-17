using System;
using System.Collections.Generic;

namespace VisualFarmStudio.Lib.ExtensionMethods
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
    }
}