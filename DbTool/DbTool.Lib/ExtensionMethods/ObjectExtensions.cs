using System.Collections;

namespace DbTool.Lib.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static bool IsCollection<T>(this T value)
        {
            return value is IEnumerable && !(value is string);
        }

        public static bool IsComplexType<T>(this T value)
        {
            var type = typeof (T);
            return type != typeof (string) && !type.IsValueType;
        }
    }
}