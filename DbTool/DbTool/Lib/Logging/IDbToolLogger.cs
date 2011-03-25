using Migrator.Framework.Loggers;

namespace DbTool.Lib.Logging
{
    public interface IDbToolLogger : ILogWriter
    {
        void WriteLine(object text);
    }
}