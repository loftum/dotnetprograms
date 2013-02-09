using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGenerator.Lib.Generating
{
    public class Record
    {
        private static readonly string Default = string.Empty;
        private readonly IList<string> _values;

        public Record(params string[] values) : this(values.ToList())
        {
            
        }

        public Record(IList<string> values)
        {
            _values = values;
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