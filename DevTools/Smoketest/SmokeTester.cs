using System.IO;
using System.Net;

namespace Smoketest
{
    public class SmokeTester
    {
        public static string SmokeTest(SmoketestArgs args)
        {
            var request = WebRequest.Create(args.Url);
            request.Method = args.Verb;
            using (var response = request.GetResponse())
            {
                if (args.Verbose)
                {
                    var stream = response.GetResponseStream();
                    if (stream == null)
                    {
                        return "ReponseStream is NULL. Wat.";
                    }
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                return ResultFor(response);
            }
        }

        private static string ResultFor(WebResponse response)
        {
            var httpResponse = response as HttpWebResponse;
            return httpResponse != null
                ? string.Format("{0} {1}", httpResponse.StatusCode.ToString("D"), httpResponse.StatusCode)
                : "OK";
        }
    }
}