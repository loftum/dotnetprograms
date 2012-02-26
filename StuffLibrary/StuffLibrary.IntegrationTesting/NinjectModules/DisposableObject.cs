using System;
using Ninject.Infrastructure.Disposal;

namespace StuffLibrary.IntegrationTesting.NinjectModules
{
    public class DisposableObject : INotifyWhenDisposed
    {
        public event EventHandler Disposed;

        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }
    }
}