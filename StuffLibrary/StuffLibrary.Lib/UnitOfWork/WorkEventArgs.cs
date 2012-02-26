using System;

namespace StuffLibrary.Lib.UnitOfWork
{
    public class WorkEventArgs : EventArgs
    {
        public bool WasComplete { get; private set; }

        public WorkEventArgs(bool wasComplete)
        {
            WasComplete = wasComplete;
        }
    }
}