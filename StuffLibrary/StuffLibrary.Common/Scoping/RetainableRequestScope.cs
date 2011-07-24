using System;
using System.Web;
using System.Collections.Generic;
using Ninject.Infrastructure.Disposal;

namespace StuffLibrary.Common.Scoping
{
    public class RetainableRequestScope : INotifyWhenDisposed
    {
        private static readonly object Lock = new object();
        private static readonly IDictionary<HttpContext, RetainableRequestScope> Scopes = new Dictionary<HttpContext, RetainableRequestScope>();

        public static RetainableRequestScope Current
        {
            get
            {
                lock (Lock)
                {
                    var currentContext = GetCurrentContextOrThrow();
                    var currentScope = GetCurrent(currentContext);
                    if (currentScope == null)
                    {
                        currentScope = new RetainableRequestScope();
                        Scopes[currentContext] = currentScope;
                    }
                    return currentScope;
                }
            }
        }

        public static void EndOfScope()
        {
            lock (Lock)
            {
                var currentContext = HttpContext.Current;
                if (currentContext == null)
                {
                    return;
                }
                var currentScope = GetCurrent(currentContext);
                if (currentScope != null && !currentScope.IsDisposed)
                {
                    currentScope.Dispose();
                }
                Scopes.Remove(currentContext);
            }
        }

        private static HttpContext GetCurrentContextOrThrow()
        {
            var currentContext = HttpContext.Current;
            if (currentContext == null)
            {
                throw new ApplicationException("HttpContext is null. Unable to create DI-scope.");
            }
            return currentContext;
        }

        private static RetainableRequestScope GetCurrent(HttpContext context)
        {
            try
            {
                return Scopes[context];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public event EventHandler Disposed;
        public bool IsDisposed { get; private set; }
        public bool Retained
        {
            get
            {
                return _owners.Count > 0;
            }
        }

        private readonly object _lock = new object();
        private readonly IList<object> _owners = new List<object>();
        private bool _disposeHasBeenCalled;

        private RetainableRequestScope()
        {
            IsDisposed = false;
            _disposeHasBeenCalled = false;
        }

        public void RetainFor(object owner)
        {
            lock (_lock)
            {
                _owners.Add(owner);
            }
        }

        public void ReleaseFor(object owner)
        {
            lock (_lock)
            {
                _owners.Remove(owner);
                if (!Retained && _disposeHasBeenCalled)
                {
                    DoDispose();
                }
            }
        }

        public void Dispose()
        {
            lock (_lock)
            {
                _disposeHasBeenCalled = true;
                if (Retained)
                {
                    return;
                }
                DoDispose();
            }
        }

        private void DoDispose()
        {
            lock (_lock)
            {
                if (!IsDisposed)
                {
                    IsDisposed = true;
                    if (Disposed != null)
                    {
                        Disposed(this, EventArgs.Empty);
                    }
                }
            }
        }
    }
}