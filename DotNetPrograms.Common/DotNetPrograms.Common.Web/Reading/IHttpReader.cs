namespace DotNetPrograms.Common.Web.Reading
{
    public interface IHttpReader
    {
        string Accept { get; set; }
        string Get(string url);
        string GetWithBasicAuth(string url, string username, string password);
    }
}