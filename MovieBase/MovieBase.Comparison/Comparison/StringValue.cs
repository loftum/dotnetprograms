using System;
using System.Text.RegularExpressions;

namespace MovieBase.Common.Comparison
{
    public class StringValue : Value<string>
    {
        private bool _ignoreCase;

        public StringValue(string value) : base(value)
        {
        }

        public static StringValue Of(string value)
        {
            return new StringValue(value);
        }

        public StringValue IgnoreCase()
        {
            _ignoreCase = true;
            return this;
        }

        public bool Matches(string pattern)
        {
            return GetRegex(pattern).IsMatch(GetValue());
        }

        private Regex GetRegex(string pattern)
        {
            return _ignoreCase ?
                new Regex(pattern, RegexOptions.IgnoreCase) :
                new Regex(pattern);
        }

        public bool EqualsIgnoreCase(string value)
        {
            return GetValue().Equals(value, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Like(string value)
        {
            return GetValue().ToLowerInvariant()
                .Contains(value.ToLowerInvariant());
        }
    }
}