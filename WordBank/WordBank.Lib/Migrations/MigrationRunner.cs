using WordBank.Migrations;
using Wordbank.Lib.Config;

namespace Wordbank.Lib.Migrations
{
    public class MigrationRunner
    {
        private readonly IWordBankSettings _settings;

        public MigrationRunner(IWordBankSettings settings)
        {
            _settings = settings;
        }

        public void MigrateToLatest()
        {
            var migrator = new Migrator.Migrator(_settings.DatabaseProvider, _settings.ConnectionString,
                                                 typeof (M000_VersionZero).Assembly);
            migrator.MigrateToLastVersion();
        }
    }
}