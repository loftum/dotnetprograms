using System.Collections.Generic;

namespace CodeGenerator.Lib.Generating
{
    public interface IInputParser
    {
        IEnumerable<Record> Parse(string input, int linesPerRecord, string delimiter);
    }
}