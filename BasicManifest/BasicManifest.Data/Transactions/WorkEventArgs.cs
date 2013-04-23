using System;

namespace BasicManifest.Data.Transactions
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