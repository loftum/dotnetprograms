using System;

namespace StuffLibrary.Lib.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IWork Begin();
    }
}