using System.Collections;

namespace DbTool.Lib.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static bool ShouldBeViewedInTable<T>(this T value)
        {
            return value is IEnumerable && !(value is string);
        }
    }
}