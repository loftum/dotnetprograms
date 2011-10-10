using System;

namespace DbTool.Lib.Tasks
{
    public class TaskProgressEventArgs : EventArgs
    {
        public int Percent { get; private set; }

        public TaskProgressEventArgs(int percent)
        {
            Percent = percent;
        }
    }
}