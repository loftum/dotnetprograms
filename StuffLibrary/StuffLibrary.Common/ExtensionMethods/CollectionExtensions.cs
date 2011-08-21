using System.Collections.Generic;
using System.Linq;

namespace StuffLibrary.Common.ExtensionMethods
{
    public static class CollectionExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> otherItems)
        {
            foreach (var otherItem in otherItems)
            {
                list.Add(otherItem);
            }
        }

        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return items.Count() == 0;
        }

        public static bool IsEmpty<T>(this IList<T> items)
        {
            return items.Count == 0;
        }

        public static string ToJavaScript<T>(this IEnumerable<T> items)
        {
            if (items == null || items.IsEmpty())
            {
                return "[]";
            }
            var quotedItems = from i in items select string.Format("\"{0}\"", i);
            return string.Format("[{0}]", string.Join(",", quotedItems));
        }
    }
}