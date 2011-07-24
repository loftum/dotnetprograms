using System.Collections.Generic;

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
    }
}