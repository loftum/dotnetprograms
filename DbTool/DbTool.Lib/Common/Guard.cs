using System;
using System.Linq.Expressions;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Common
{
    public class Guard
    {
        public static void NotNull<T>(params Expression<Func<T>>[] args) where T : class
        {
            foreach (var arg in args)
            {
                var name = arg.GetPropertyId();
                var value = arg.Compile().Invoke();
                if (value == null)
                {
                    throw new ArgumentNullException(name);
                }
            }
        }

        public static void NotNullOrEmpty(params Expression<Func<string>>[] args)
        {
            foreach (var arg in args)
            {
                var name = arg.GetPropertyId();
                var value = arg.Compile().Invoke();
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(name, string.Format("{0} cannot be null or empty", name));
                }    
            }
        }
    }
}