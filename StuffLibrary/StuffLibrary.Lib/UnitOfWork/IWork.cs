using System;

namespace StuffLibrary.Lib.UnitOfWork
{
    public delegate void WorkEndedEventHandler(IWork sender, WorkEventArgs e);

    public interface IWork : IDisposable
    {
        event WorkEndedEventHandler Ended;
        bool HasEnded { get; }
        bool IsComplete { get; }
        void Complete();
    }
}