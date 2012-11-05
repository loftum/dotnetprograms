using Ninject.Syntax;
using StuffLibrary.Common.DependencyInjection;

namespace StuffLibrary.Common.ExtensionMethods
{
    public static class NinjectExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> InCurrentScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> syntax)
        {
            return syntax.InScope(c => InjectionScope.Current);
        }
    }
}