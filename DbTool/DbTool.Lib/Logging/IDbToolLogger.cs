namespace DbTool.Lib.Logging
{
    public interface IDbToolLogger
    {
        void Write(string format, params object[] args);
        void WriteLine(string format, params object[] args);
        void WriteLine(object text);
    }
}