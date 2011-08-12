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

        public static string ToJavaScript<T>(this IEnumerable<T> items)
        {
            var quotedItems = from i in items select string.Format("\"{0}\"", i);
            return string.Format("[{0}]", string.Join(",", quotedItems));
        }
    }
}