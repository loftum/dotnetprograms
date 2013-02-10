using System.Collections.Generic;
using System.Linq;
using CodeGenerator.Lib.Text;

namespace CodeGenerator.Lib.Generating
{
    public class InputParser : IInputParser
    {
        public IEnumerable<Record> Parse(string input, int linesPerRecord, string delimiter)
        {
            var blocks = new TextTraverser(input).ReadBlocksOfLines(linesPerRecord);
            return blocks.Select(b => new Record(b, delimiter));
        }
    }
}