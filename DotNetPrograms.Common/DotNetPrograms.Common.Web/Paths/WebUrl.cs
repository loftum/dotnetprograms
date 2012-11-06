using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using DotNetPrograms.Common.ExtensionMethods;

namespace DotNetPrograms.Common.Web.Paths
{
    public class WebUrl
    {
        public const string DefaultProtocol = "http";
        public const int DefaultHttpPort = 80;
        private const string UrlPattern = @"(([\w]+)://)?(([\w\.]+)(:([\d]*))?)?/?([^\s^?]+/?)?[\?]?([^\s]+)?";

        private static readonly Dictionary<string, int> Ports = new Dictionary<string, int>
            {
                { "http", 80 },
                { "https", 443 }
            };

        public string Protocol { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool HasDefaultPort
        {
            get { return Port == DefaultPortFor(Protocol); }
        }

        public string Hash { get; set; }
        public bool HasHash { get { return !Hash.IsNullOrWhiteSpace(); } }
        private bool _hasHashMark;
        public bool HasHashMark
        {
            get { return _hasHashMark || HasHash; }
            set { _hasHashMark = value; }
        }

        public string HostAndPort
        {
            get
            {
                return HasDefaultPort
                    ? Host
                    : string.Format("{0}:{1}", Host, Port);
            }
        }

        private readonly IList<string> _pathParts = new List<string>();
        public string Path
        {
            get { return string.Join("/", _pathParts); }
            set
            { 
                _pathParts.Clear();
                if (!value.IsNullOrWhiteSpace())
                {
                    _pathParts.AddRange(VirtualPath.Split(value));
                }
            }
        }
        public bool HasPath { get { return _pathParts.Any(); } }
        private string _extension;
        public string Extension
        {
            get { return _extension; }
            set
            {
                _extension = value.IsNullOrEmpty() ? value : value.Replace(".", string.Empty).Trim();
            }
        }
        public bool HasExtension { get { return !Extension.IsNullOrWhiteSpace(); } }

        public IDictionary<string, string> Parameters { get; private set; }
        public bool HasParameters { get { return Parameters.Any(); } }

        public WebUrl()
            : this(string.Empty)
        {
        }

        public WebUrl(string url)
        {
            Parameters = new Dictionary<string, string>();
            Protocol = DefaultProtocol;
            Port = DefaultHttpPort;
            var trimmedUrl = (url ?? string.Empty).Trim();
            Parse(trimmedUrl);
        }

        private void Parse(string url)
        {
            var hashIndex = url.IndexOf('#');
            HasHashMark = hashIndex >= 0;
            Hash = HasHashMark ? url.Substring(hashIndex + 1) : string.Empty;
            var regularUrl = HasHashMark ? url.Substring(0, hashIndex) : url;
            ParseRegularUrl(regularUrl);
        }

        private void ParseRegularUrl(string url)
        {
            var match = Regex.Match(url, UrlPattern);
            var groups = match.Groups;
            Protocol = groups[2].Value.OrIfNullOrEmpty(DefaultProtocol);
            Host = groups[4].Value;
            Port = groups[6].Value.ConvertToOrDefault(DefaultPortFor(Protocol));
            Path = groups[7].Value;
            Parameters = ParseUrlParams(groups[8].Value);
        }

        private static Dictionary<string, string> ParseUrlParams(string parameters)
        {
            var dict = new Dictionary<string, string>();
            var urlParams = parameters.Split('&');
            foreach (var param in urlParams)
            {
                var keyValue = param.Split('=');
                if (keyValue.Length == 2)
                {
                    var key = keyValue[0].ToLowerInvariant();
                    var decodedKey = UrlDecode(key);
                    var decodedValue = UrlDecode(keyValue[1]);
                    dict[decodedKey] = decodedValue;
                }
            }
            return dict;
        }

        public static int DefaultPortFor(string protocol)
        {
            return Ports.ContainsKey(protocol) ? Ports[protocol] : DefaultHttpPort;
        }

        private static string UrlEncode(string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        private static string UrlDecode(string value)
        {
            return HttpUtility.UrlDecode(value);
        }

        private string GetParameterString()
        {
            if (!HasParameters)
            {
                return string.Empty;
            }

            var keysAndValues = string.Join("&", Parameters.Select(p => string.Format("{0}={1}", UrlEncode(p.Key), UrlEncode(p.Value))));
            return string.Format("?{0}", keysAndValues);
        }

        public WebUrl WithHost(string host)
        {
            Host = host;
            return this;
        }

        public WebUrl WithPort(int port)
        {
            Port = port;
            return this;
        }

        public WebUrl WithParameter(string name, Guid value)
        {
            return WithParameter(name, value.ToString());
        }

        public WebUrl WithParameter(string name, int value)
        {
            return WithParameter(name, value.ToInvariantString());
        }

        public WebUrl WithParameter(string name, string value)
        {
            Parameters[name] = value;
            return this;
        }

        public WebUrl WithParameters(IDictionary<string, object> parameters)
        {
            foreach (var p in parameters)
            {
                Parameters[p.Key] = p.Value.ToString();
            }
            return this;
        }

        public WebUrl WithPath(string path)
        {
            Path = path;
            return this;
        }

        public WebUrl AppendToPath(string path)
        {
            _pathParts.AddRange(VirtualPath.Split(path));
            return this;
        }

        public WebUrl WithExtension(string extension)
        {
            Extension = extension;
            return this;
        }

        public WebUrl WithProtocol(string protocol)
        {
            Protocol = protocol;
            return this;
        }

        public WebUrl ForceSsl(bool forceSsl)
        {
            return forceSsl ? WithSsl(true) : this;
        }

        public WebUrl WithSsl(bool ssl)
        {
            var keepDefaultPort = HasDefaultPort;
            var protocol = ssl ? "https" : "http";
            var port = keepDefaultPort ? DefaultPortFor(protocol) : Port;
            return WithProtocol(protocol).WithPort(port);
        }

        public WebUrl ClearParameters()
        {
            Parameters.Clear();
            return this;
        }

        public WebUrl WithHash(string hash)
        {
            Hash = hash;
            return this;
        }

        public WebUrl WithHashMark(bool hasHashMark)
        {
            HasHashMark = hasHashMark;
            return this;
        }

        public string GetRootUrl()
        {
            var builder = new StringBuilder()
               .AppendFormat("{0}://", Protocol)
               .Append(HostAndPort);
            return builder.ToString();
        }

        public override string ToString()
        {
            var builder = new StringBuilder()
                .AppendFormat("{0}://", Protocol)
                .Append(HostAndPort);

            if (HasPath)
            {
                builder.AppendFormat("/{0}", Path);
            }

            if (HasExtension)
            {
                builder.AppendFormat(".{0}", Extension);
            }

            builder.Append(GetParameterString());

            if (HasHashMark)
            {
                builder.Append("#");
            }

            if (HasHash)
            {
                builder.Append(Hash);
            }
            return builder.ToString();
        }
    }
}