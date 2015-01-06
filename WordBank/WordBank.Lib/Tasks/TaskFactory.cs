using Wordbank.Lib.Data;
using Wordbank.Lib.Logging;

namespace WordBank.Lib.Tasks
{
    public class TaskFactory : ITaskFactory
    {
        private readonly IWordBankRepository _repo;
        private readonly IWordBankParser _parser;
        private readonly IWordBankLogger _logger;

        public TaskFactory(IWordBankRepository repo, IWordBankParser parser, IWordBankLogger logger)
        {
            _repo = repo;
            _parser = parser;
            _logger = logger;
        }

        public IImportTask CreateImportTask()
        {
            return new ImportTask(_repo, _parser, _logger);
        }
    }
}