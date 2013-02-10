using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Lib.Text;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Generating
{
    public class Record
    {
        public TextBlock Block { get; private set; }
        private static readonly string Default = string.Empty;
        private readonly IList<string> _values = new List<string>();

        public Record(TextBlock block, string delimiter)
        {
            Block = block;
            foreach (var values in block.Lines.Select(line => Regex.Split(line, delimiter)))
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