namespace StuffLibrary.Lib.UnitOfWork
{
    public class Work : IWork
    {
        public event WorkEndedEventHandler Ended;
        public bool HasEnded { get; private set; }
        public bool IsComplete { get; private set; }

        public void Complete()
        {
            IsComplete = true;
        }

        public void Dispose()
        {
            HasEnded = true;
            
            if (Ended != null)
            {
                Ended(this, new WorkEventArgs(IsComplete));
            }
        }
    }
}