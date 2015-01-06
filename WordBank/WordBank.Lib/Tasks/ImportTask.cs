using System.IO;
using System.Text;
using Wordbank.Lib.Data;
using Wordbank.Lib.Domain;
using Wordbank.Lib.Logging;

namespace WordBank.Lib.Tasks
{
    public class ImportTask : IImportTask
    {
        private readonly IWordBankRepository _repo;
        private readonly IWordBankParser _parser;
        private readonly IWordBankLogger _logger;

        public ImportTask(IWordBankRepository repo, IWordBankParser parser, IWordBankLogger logger)
        {
            _repo = repo;
            _parser = parser;
            _logger = logger;
        }

        public void Import(string paradigmeFile, string fullFormFile)
        {
            _logger.Info("Importing paradigmeFile: {0}, fullFormFile: {1}", paradigmeFile, fullFormFile);
            ImportParadigme(paradigmeFile);
            ImportFullForm(fullFormFile);
        }

        public void ImportFullForm(string fullFormFile)
        {
            ImportAndStore<Ord>(fullFormFile);
        }

        public void ImportParadigme(string paradigmeFile)
        {
            ImportAndStore<Paradigme>(paradigmeFile);
        }

        private void ImportAndStore<T>(string filename) where T : DomainObject, new()
        {
            _logger.Info("Importing file: {0}", filename);
            var current = 0;

            using (var streamReader = new StreamReader(filename, Encoding.UTF7))
            {
                while (!streamReader.EndOfStream)
                {
                    current++;
                    var line = streamReader.ReadLine();
                    if (!ShouldIgnore(line))
                    {
                        ParseAndSave<T>(line);

                    }
                    if (current % 50000 == 0)
                    {
                        _logger.Info("Row {0}", current);
                    }
                }
            }    
            _logger.Info("Done with file {0}", filename);
        }

        private static bool ShouldIgnore(string line)
        {
            return string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("*");
        }

        private void ParseAndSave<T>(string line) where T : DomainObject, new()
        {
            var split = line.Split(new[] {'\t'});
            var paradigme = _parser.Parse<T>(split);
            _repo.Save(paradigme);
        }
    }
}