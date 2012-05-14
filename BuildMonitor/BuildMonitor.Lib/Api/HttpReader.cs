using System.IO;
using System.Net;
using BuildMonitor.Common.ExtensionMethods;

namespace BuildMonitor.Lib.Api
{
    public class HttpReader : IHttpReader
    {
        public string ReadJsonBasichAuth(string url, string username, string password)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var auth = string.Format("{0}:{1}", username, password).ToBase64();
            request.Headers["Authorization"] = string.Format("Basic {0}", auth);
            request.Method = "GET";
            request.Accept = "application/json";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                    {
                        return string.Empty;
                    }
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}