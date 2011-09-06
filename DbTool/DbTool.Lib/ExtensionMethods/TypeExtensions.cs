using System;
using System.Linq;

namespace DbTool.Lib.ExtensionMethods
{
    public static class TypeExtensions
    {
        public static bool IsOneOf(this Type type, params Type[] otherTypes)
        {
            if (otherTypes.Any(type.Equals))
            {
                return true;
            }
            return false;
        }
    }
}