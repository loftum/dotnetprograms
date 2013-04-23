using System;

namespace BasicManifest.Data.Transactions
{
    public delegate void WorkEndedEventHandler(WorkUnit sender, WorkEventArgs e);

    public class WorkUnit : IDisposable
    {
        private event WorkEndedEventHandler _ended;

        public event WorkEndedEventHandler Ended
        {
            add { _ended += value; }
            remove { _ended -= value; }
        }

        public bool HasEnded { get; private set; }
        public bool IsComplete { get; private set; }

        public void Dispose()
        {
            HasEnded = true;
            if (_ended != null)
            {
                _ended(this, new WorkEventArgs(IsComplete));
            }
            _ended = null;
        }

        public void Complete()
        {
            IsComplete = true;
        }
    }
}