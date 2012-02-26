using Ninject.Syntax;

namespace StuffLibrary.Common.Scoping
{
    public static class NinjectExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> InCurrentInjectionScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> binding)
        {
            return binding.InScope(context => InjectionContext.Current);
        }
    }
}