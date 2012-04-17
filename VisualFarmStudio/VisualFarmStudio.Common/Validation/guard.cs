using System;
using System.Linq.Expressions;
using VisualFarmStudio.Common.ExtensionMethods;

namespace VisualFarmStudio.Common.Validation
{
    public class Guard
    {
        public static void NotNull<T>(params Expression<Func<T>>[] parameters) where T : class
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
    }
}