using System;
using System.Net;

namespace DotNetPrograms.Common.Web.Exceptions
{
    public class FriendlyWebException : Exception
    {
        public WebException WebException { get; private set; }
        public string Url { get; private set; }

        public FriendlyWebException(string url, WebException webException)
        {
            Url = url;
            WebException = webException;
        }

        public FriendlyWebException(Uri url, WebException webException)
        {
            Url = url.ToString();
            WebException = webException;
        }
    }
}