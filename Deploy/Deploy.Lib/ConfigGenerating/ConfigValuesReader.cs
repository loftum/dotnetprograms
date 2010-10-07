using System.Collections.Generic;
using System.Linq;
using Deploy.Lib.Readers;

namespace Deploy.Lib.ConfigGenerating
{
    public class ConfigValuesReader
    {
        private readonly IFileReader _fileReader;

        public ConfigValuesReader(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public IDictionary<string, string> GetValues(string configValuesFilePath)
        {
            var lines = _fileReader.ReadLines(configValuesFilePath);
            return Parse(lines);
        }

        private static IDictionary<string, string> Parse(IEnumerable<string> lines)
        {
            return lines.Select(line => line.Split(new[] {'='}, 2))
                .ToDictionary(keyAndValue => keyAndValue[0], keyAndValue => keyAndValue[1]);
        }
    }
}
