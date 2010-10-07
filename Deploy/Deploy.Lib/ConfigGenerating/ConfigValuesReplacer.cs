using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Deploy.Lib.Readers;

namespace Deploy.Lib.ConfigGenerating
{
    public class ConfigValuesReplacer
    {
        private readonly IDictionary<string, string> _configValues;
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;

        public ConfigValuesReplacer(IDictionary<string, string> configValues, IFileReader fileReader, IFileWriter fileWriter)
        {
            _configValues = configValues;
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        public void ReplaceIn(string filePath)
        {
            var replacedText = GetReplacedText(filePath);
            _fileWriter.Write(replacedText, filePath);
        }

        private string GetReplacedText(string filePath)
        {
            var fileContent = _fileReader.ReadAll(filePath);
            foreach (var configValue in _configValues)
            {
                var regex = new Regex(PatternFor(configValue.Key));
                foreach (var match in regex.Matches(fileContent))
                {
                    fileContent = fileContent.Replace(match.ToString(), Format(configValue));
                }
            }
            return fileContent;
        }

        private string Format(KeyValuePair<string, string> configValue)
        {
            return new StringBuilder(configValue.Key).Append("=")
                .Append(Fnutt(configValue.Value))
                .ToString();
        }

        private string Fnutt(string value)
        {
            var builder = new StringBuilder();
            if (!value.StartsWith("\""))
            {
                builder.Append("\"");
            }
            builder.Append(value);
            if (!value.EndsWith("\""))
            {
                builder.Append("\"");
            }
            return builder.ToString();
        }

        private static string PatternFor(string key)
        {
            return new StringBuilder(key).Append("=").Append("\"{1}.*\"{1}").ToString();
        }
    }
}
