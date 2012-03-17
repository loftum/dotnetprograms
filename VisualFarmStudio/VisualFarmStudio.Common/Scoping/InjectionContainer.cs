using Ninject;

namespace VisualFarmStudio.Common.Scoping
{
    public class InjectionContainer
    {
        public static IKernel Kernel { private get; set; }

        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}