using System;

namespace StuffLibrary.Common.Scoping
{
    public class InjectionContext
    {
        private static Func<object> _currentScope;

        public static object Current
        {
            get { return _currentScope(); }
            set { _currentScope = () => value; }
        }

        static InjectionContext()
        {
            _currentScope = () => RetainableRequestScope.Current;
        }

        public static void SetCurrent(Func<object> currentScope)
        {
            _currentScope = currentScope;
        }
    }
}