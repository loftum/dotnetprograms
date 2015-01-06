using System.Linq;

namespace MongoTool.Core.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static bool In(this object value, params object[] values)
        {
            if (value == null)
            {
                return values == null || !values.Any();
            }
            return values.Any(v => v.Equals(value));
        }
    }
}