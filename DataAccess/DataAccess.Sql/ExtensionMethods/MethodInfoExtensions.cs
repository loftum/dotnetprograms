using System.Linq;
using System.Reflection;

namespace DataAccess.Sql.ExtensionMethods
{
    public static class MethodInfoExtensions
    {
        public static string GetFriendlyName(this MethodInfo method)
        {
            var reflectedType = method.ReflectedType.GetFriendlyName();
            var returnType = method.ReturnType.GetFriendlyName();
            var methodName = method.Name;
            var parameters = method.GetParameters().Select(p => string.Format("{0} {1}", p.ParameterType.GetFriendlyName(), p.Name));
            return string.Format("{0} {1}.{2}({3})", returnType, reflectedType, methodName, string.Join(", ", parameters));
        }
    }
}