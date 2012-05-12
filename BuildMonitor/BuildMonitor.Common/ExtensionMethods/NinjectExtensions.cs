using BuildMonitor.Common.DependencyInjection;
using Ninject.Syntax;

namespace BuildMonitor.Common.ExtensionMethods
{
    public static class NinjectExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> InCurrentScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> binding)
        {
            return binding.InScope(c => InjectionScope.Current);
        }
    }
}