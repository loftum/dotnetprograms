using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Lib.Text;

namespace CodeGenerator.Lib.Generating
{
    public class Record : TextElement
    {
        private static readonly string Default = string.Empty;
        private readonly IList<InputValue> _values = new List<InputValue>();
        public ParameterArray Values { get { return new ParameterArray(_values.Select(v => v.RawText)); } }

        public Record(TextBlock block, string delimiterPattern) : base(block.Text, block.StartIndex)
        {
            foreach (var value in GetInputValues(delimiterPattern))
            {
                _values.Add(value);
            }
        }

        private IEnumerable<InputValue> GetInputValues(string delimiterPattern)
        {
            var startIndex = 0;
            var delimiters = GetDelimiters(delimiterPattern);
            if (!delimiters.Any())
            {
                yield return new InputValue(RawText, Bias(startIndex));
                yield break;
            }
            foreach (var delimiterMatch in delimiters)
            {
                yield return new InputValue(RawText.Substring(startIndex, delimiterMatch.Index - startIndex), Bias(startIndex));
                startIndex = delimiterMatch.Index + delimiterMatch.Length;
            }

            var last = delimiters.LastOrDefault();
            if (last != null)
            {
                yield return new InputValue(RawText.Substring(startIndex, RawText.Length - startIndex), Bias(startIndex));
            }
        }

        private IList<Match> GetDelimiters(string delimiterPattern)
        {
            var regex = new Regex(delimiterPattern);
            return regex.Matches(RawText).Cast<Match>().ToList();
        }

        public string this[int index]
        {
            get
            {
                try
                {
                    return _values[index].RawText;
                }
                catch (IndexOutOfRangeException)
                {
                    return Default;
                }
                catch (ArgumentOutOfRangeException)
                {
                    return Default;
                }
            }
        }
    }
}