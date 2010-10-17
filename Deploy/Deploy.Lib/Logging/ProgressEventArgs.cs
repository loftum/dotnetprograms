namespace Deploy.Lib.Logging
{
    public class ProgressEventArgs
    {
        public int Total { get; private set; }
        public int Current { get; private set; }
        public double Percentage { get { return (double) Current/Total; } }

        public ProgressEventArgs(int total, int current)
        {
            Total = total;
            Current = current;
        }
    }
}