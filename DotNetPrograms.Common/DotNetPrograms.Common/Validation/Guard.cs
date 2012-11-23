using System;
using System.Linq.Expressions;
using DotNetPrograms.Common.ExtensionMethods;

namespace DotNetPrograms.Common.Validation
{
    public static class Guard
    {
        public static void NotNull(params Expression<Func<object>>[] parameters)
        {
            foreach (var parameter in parameters)
            {
                var value = parameter.Compile().Invoke();
                if (value == null)
                {
                    var name = parameter.GetPropertyName();
                    throw new ArgumentNullException(string.Format("{0} cannot be null", name));
                }
            }
        }

        public static void NotNullOrEmpty(params Expression<Func<string>>[] parameters)
        {
            foreach (var parameter in parameters)
            {
                var value = parameter.Compile().Invoke();
                if (string.IsNullOrEmpty(value))
                {
                    var name = parameter.GetPropertyName();
                    throw new ArgumentNullException(string.Format("{0} cannot be null or empty", name));
                }
            }
        }

        public static void NotNullOrWhiteSpace(params Expression<Func<string>>[] parameters)
        {
            foreach (var parameter in parameters)
            {
                var value = parameter.Compile().Invoke();
                if (string.IsNullOrWhiteSpace(value))
                {
                    var name = parameter.GetPropertyName();
                    throw new ArgumentNullException(string.Format("{0} cannot be null or empty", name));
                }
            }
        }
    }
}