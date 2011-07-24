using Ninject.Syntax;

namespace StuffLibrary.Common.Scoping
{
    public static class NinjectExtensions
    {
        public static IBindingNamedWithOrOnSyntax<T> InRetainableRequestScope<T>(this IBindingWhenInNamedWithOrOnSyntax<T> binding)
        {
            return binding.InScope(context => RetainableRequestScope.Current);
        }
    }
}