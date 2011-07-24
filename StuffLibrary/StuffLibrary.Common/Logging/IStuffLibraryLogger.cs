namespace StuffLibrary.Common.Logging
{
    public interface IStuffLibraryLogger
    {
        void Info(object message);
        void Debug(object message);
        void Error(object message);
    }
}