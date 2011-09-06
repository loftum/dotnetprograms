using System.Linq;

namespace DbTool.Lib.ExtensionMethods
{
    public static class ComparisonExtensions
    {
        public static bool In(this object value, params object[] values)
        {
            if (value == null)
            {
                return values.IsNullOrEmpty();
            }
            return values.Any(v => v.Equals(value));
        }
    }
}