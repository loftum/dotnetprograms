using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotNetPrograms.Common.Collections.Chunking;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Generating
{
    public class Record
    {
        public int Number { get; private set; }
        public string Text { get; private set; }
        private static readonly string Default = string.Empty;
        private readonly IList<string> _values = new List<string>();

        public Record(Chunk<string> lines, string delimiter)
        {
            Number = lines.Number;
            Text = string.Join(Environment.NewLine, lines);
            foreach (var values in lines.Select(line => Regex.Split(line, delimiter)))
            {
                _values.AddRange(values);
            }
        }

        public string this[int index]
        {
            get
            {
                try
                {
                    return _values[index];
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