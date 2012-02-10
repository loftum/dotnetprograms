using Wordbank.Lib.Data;

namespace WordBank.Lib.Tasks
{
    public class TaskFactory : ITaskFactory
    {
        private readonly IWordBankRepository _repo;
        private readonly IWordBankParser _parser;

        public TaskFactory(IWordBankRepository repo, IWordBankParser parser)
        {
            _repo = repo;
            _parser = parser;
        }

        public IImportTask CreateImportTask()
        {
            return new ImportTask(_repo, _parser);
        }
    }
}