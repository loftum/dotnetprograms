using System;

namespace DbTool.Lib.Memory
{
    public interface IMemoryMeter : IDisposable
    {
        void Start();
        void Stop();
    }
}