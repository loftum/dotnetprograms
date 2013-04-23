using System;

namespace BasicManifest.Data.Transactions
{
    public interface IUnitOfWork : IDisposable
    {
        WorkUnit Begin();
    }
}