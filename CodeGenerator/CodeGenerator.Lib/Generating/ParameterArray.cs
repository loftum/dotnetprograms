using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeGenerator.Lib.Generating
{
    public class ParameterArray : IEnumerable<string>
    {
        private readonly IList<string> _values;

        public ParameterArray(IEnumerable<string> values)
        {
            _values = values.ToList();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(",", _values);
        }
    }
}