using System;

namespace EnvironmentViewer.Lib.Data
{
    public interface IVersionRepo : IDisposable
    {
        long GetVersion();
    }
}