using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.Collections.Chunking;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Generating
{
    public class InputParser : IInputParser
    {
        public IEnumerable<Record> Parse(string input, int linesPerRecord, string delimiter)
        {
            var chunks = GetChunks(input, linesPerRecord);
            return chunks.Select(c => new Record(c, delimiter));
        }

        private static IEnumerable<Chunk<string>> GetChunks(string text, int linesPerRecord)
        {
            var lines = text.SplitLines(true);
            return lines.InChunksOf(linesPerRecord);
        }
    }
}