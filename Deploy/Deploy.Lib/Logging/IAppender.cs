namespace Deploy.Lib.Logging
{
    public interface IAppender
    {
        void Append(object sender, LogMessageEventArgs args);
    }
}