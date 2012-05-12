using System.Collections.Generic;

namespace BuildMonitor.Lib.Configuration
{
    public interface IBuildServerSettings
    {
        string Host { get; }
        string Username { get; }
        string Password { get; }
        IEnumerable<string> ProjectIds { get; }
    }
}