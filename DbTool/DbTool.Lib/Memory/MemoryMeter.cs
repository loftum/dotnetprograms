using System;
using System.Timers;

namespace DbTool.Lib.Memory
{
    public class MemoryMeter : IMemoryMeter
    {
        private Timer _timer;
        private readonly Action<long> _action;

        public MemoryMeter(Action<long> action)
        {
            _action = action;
            _timer = new Timer(5000);
            _timer.Elapsed += HandleElapsed;
        }

        private void HandleElapsed(object sender, ElapsedEventArgs e)
        {
            var usage = GC.GetTotalMemory(true);
            _action(usage);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;    
            }
        }
    }
}