using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Deploy.Lib.RegularExpressions
{
    public class Regexifier
    {
        private readonly StringBuilder _builder;

        public string Pattern
        {
            get { return _builder.ToString(); }
        }

        private Regexifier(string patternCandidate)
        {
            _builder = new StringBuilder(patternCandidate);
        }

        public static Regexifier Regexify(string patternCandidate)
        {
            return new Regexifier(patternCandidate).DoRegexify();
        }

        private Regexifier DoRegexify()
        {
            if (!PatternIsValid())
            {
                _builder.Replace(@"*", @"\.*")
                .Replace(@".", @"\.").ToString();
            }
            return this;
        }

        public Regexifier WithStartAndEnd()
        {
            if (!_builder.ToString().StartsWith("^"))
            {
                _builder.Insert(0, "^");
            }
            if (!_builder.ToString().EndsWith("$"))
            {
                _builder.Insert(_builder.Length, "$");
            }
            return this;
        }

        public bool PatternIsValid()
        {
            try
            {
                new Regex(_builder.ToString()).IsMatch("");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Regexifier WithTrailingWildcard()
        {
            _builder.Append(@"\.*");
            return this;
        }
    }
}