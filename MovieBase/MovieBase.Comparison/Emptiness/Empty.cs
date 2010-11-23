using System.Collections.Generic;

namespace MovieBase.Common.Emptiness
{
    public class Empty
    {
        public static IEnumerable<T> IEnumerable<T>()
        {
            return IList<T>();
        }

        public static T[] Array<T>()
        {
            return new T[0];
        }

        public static IList<T> IList<T>()
        {
            return List<T>();
        }

        public static List<T> List<T>()
        {
            return new List<T>();
        }

        public static string String()
        {
            return string.Empty;
        }
    }
}