using System.IO;
using Wordbank.Lib.Data;
using Wordbank.Lib.Domain;

namespace WordBank.Lib.Tasks
{
    public class ImportTask : IImportTask
    {
        private readonly IWordBankRepository _repo;
        private readonly IWordBankParser _parser;

        public ImportTask(IWordBankRepository repo, IWordBankParser parser)
        {
            _repo = repo;
            _parser = parser;
        }

        public void Import(string paradigmeFile, string fullFormFile)
        {
            ImportAndStore<Paradigme>(paradigmeFile);
            ImportAndStore<Ord>(fullFormFile);
        }

        private void ImportAndStore<T>(string filename) where T : DomainObject, new()
        {
            using (var streamReader = File.OpenText(filename))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine();
                    if (!ShouldIgnore(line))
                    {
                        ParseAndSave<T>(line);
                    }
                }
            }
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