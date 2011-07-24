using System.Collections.Generic;

namespace StuffLibrary.UnitTesting.Asserting
{
    public class CustomAssert
    {
         public static CollectionAsserter<T> ThatCollection<T>(IEnumerable<T> collection)
         {
             return new CollectionAsserter<T>(collection);
         }
    }
}