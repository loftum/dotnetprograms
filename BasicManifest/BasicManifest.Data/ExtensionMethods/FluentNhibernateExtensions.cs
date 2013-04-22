using System;

namespace BasicManifest.Data.ExtensionMethods
{
    public static class FluentNhibernateExtensions
    {
        public static T If<T>(this T item, bool condition, Action<T> action)
        {
            if (condition)
            {
                action(item);
            }
            return item;
        }
    }
}