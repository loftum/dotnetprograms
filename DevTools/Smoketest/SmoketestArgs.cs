using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Smoketest
{
    public class SmoketestArgs
    {
        public string Verb { get; set; }
        public string Url { get; set; }
        public bool Verbose { get; set; }

        public SmoketestArgs(string verb, string url)
        {
            Verb = verb;
            Url = Regex.IsMatch(url, @"[^\s]*://[\.]*") ? url : string.Format("http://{0}", url);
        }

        public SmoketestArgs(string url) : this("GET", url)
        {
        }

        public static SmoketestArgs Parse(string[] argz)
        {
            if (argz.Length < 1)
            {
                throw new ArgumentException("No URL specified");
            }

            var smoketestArguments = argz.Where(a => !a.StartsWith("-")).ToArray();
            var switches = argz.Where(a => a.StartsWith("-")).ToArray();

            var arguments = smoketestArguments.Length < 2
                ? new SmoketestArgs(argz[0])
                : new SmoketestArgs(argz[0], argz[1]);

            arguments.Verbose = switches.Contains("-v");
            return arguments;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Verb, Url);
        }
    }
}