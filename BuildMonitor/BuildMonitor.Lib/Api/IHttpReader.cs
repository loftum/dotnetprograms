namespace BuildMonitor.Lib.Api
{
    public interface IHttpReader
    {
        string ReadJsonBasichAuth(string url, string username, string password);
    }
}