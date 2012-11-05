using System;
using System.IO;
using System.Net;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Web.Exceptions;

namespace DotNetPrograms.Common.Web.Reading
{
    public class HttpReader : IHttpReader
    {
        public string Accept { get; set; }

        public HttpReader()
        {
            Accept = "application/json";
        }

        public string GetWithBasicAuth(string url, string username, string password)
        {
            var request = WebRequest.Create(url);
            var auth = string.Format("{0}:{1}", username, password).ToBase64();
            request.Headers["Authorization"] = string.Format("Basic {0}", auth);
            request.Method = "GET";
            return GetResponse((dynamic) request);
        }

        public string Get(string url)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";
            return GetResponse((dynamic) request);
        }

        private string GetResponse(HttpWebRequest request)
        {
            request.Accept = Accept;
            request.Timeout = 20000;
            request.ReadWriteTimeout = 20000;
            try
            {
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
                            var read = 0;
                            var buffer = new char[1000000];
                            while (!reader.EndOfStream && read < buffer.Length)
                            {
                                buffer[read] = (char)reader.Read();
                                read++;
                            }
                            response.Close();
                            return new string(buffer);
                        }
                    }
                }
            }
            catch (WebException we)
            {
                throw new FriendlyWebException(request.RequestUri.ToString(), we);
            }
        }

        private static string GetResponse(object invalid)
        {
            throw new Exception(string.Format("Don't know how to read a {0}", invalid.GetType()));
        }
    }
}