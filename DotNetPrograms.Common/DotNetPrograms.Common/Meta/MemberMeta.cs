using System.Reflection;

namespace DotNetPrograms.Common.Meta
{
    public abstract class MemberMeta
    {
        public abstract string Name { get; }

        protected bool IsProxiableMethod(MethodInfo method)
        {
            return new MethodMeta(method).IsProxiable;
        }
    }
}