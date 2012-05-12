namespace BuildMonitor.Common.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static T[] AsArray<T>(this T item)
        {
            return new []{item};
        }
    }
}