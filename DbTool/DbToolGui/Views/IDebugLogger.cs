namespace DbToolGui.Views
{
    public interface IDebugLogger
    {
        void Log(string text, params object[] args);
    }
}