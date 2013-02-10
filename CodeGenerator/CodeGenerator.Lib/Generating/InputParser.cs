using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotNetPrograms.Common.ExtensionMethods;

namespace CodeGenerator.Lib.Generating
{
    public class InputParser : IInputParser
    {
        public IEnumerable<Record> Parse(string input, int linesPerRecord, string delimiter)
        {
            return GetRecords(input, linesPerRecord)
                .Select(t => Split(t, delimiter))
                .Select(values => new Record(values));
        }

        private static IList<string> Split(string recordText, string delimiter)
        {
            return Regex.Split(recordText, delimiter);
        }

        private static IEnumerable<string> GetRecords(string text, int linesPerRecord)
        {
            var lines = text.SplitLines(true);
            var chunks = lines.InChunksOf(linesPerRecord);

            return chunks.Select(GetRecord);
        }

        private static string GetRecord(IEnumerable<string> chunk)
        {
            return string.Join(" ", chunk);
        }
    }
}