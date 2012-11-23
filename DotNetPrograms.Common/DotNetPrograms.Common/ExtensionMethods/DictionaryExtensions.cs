using System.Collections.Generic;
using DotNetPrograms.Common.Validation;

namespace DotNetPrograms.Common.ExtensionMethods
{
    public static class DictionaryExtensions
    {
        public static T Get<T>(this IDictionary<string, object> dictionary, string key)
        {
            Guard.NotNull(() => dictionary, () => key);
            return dictionary[key].ConvertTo<T>();
        }
    }
}