using System.Collections.Generic;

namespace DbTool.Testing.Assertions
{
    public class MakeSure
    {
        public static CollectionAsserter<T> ThatCollection<T>(IEnumerable<T> collection)
        {
            return new CollectionAsserter<T>(collection);
        }
    }
}