using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Find.Parameters
{
    public class FindParameters
    {
        public string Path { get; private set; }
        public string Pattern { get; private set; }
        public bool ShowErrors { get; private set; }

        private FindParameters(string path, string pattern, bool showErrors)
        {
            Path = path;
            Pattern = pattern;
            ShowErrors = showErrors;
        }

        public static FindParameters Parse(string[] args)
        {
            return args.Length < 2 ?
                new FindParameters(".", ToRegex(args[0]), false) : 
                new FindParameters(args[0], ToRegex(args[1]), TrueIfExists(args, "-e"));
        }

        private static bool TrueIfExists(IEnumerable<string> args, string flag)
        {
            return args.Contains(flag);
        }

        private static string ToRegex(string patternCandidate)
        {
            return IsValidRegex(patternCandidate) ? 
            patternCandidate : 
            Regexify(patternCandidate);
        }

        private static string Regexify(string patternCandidate)
        {
            return new StringBuilder(patternCandidate)
                .Replace("*", @".*").Replace(".", @"\.")
                .ToString();
        }

        private static bool IsValidRegex(string patternCandidate)
        {
            try
            {
                new Regex(patternCandidate).Match(string.Empty);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
