using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Lib.Data
{
    public class BuildMonitorFileRepo : IBuildMonitorRepo
    {
        private readonly IBuildMonitorSettings _settings;
        private readonly IFileManager _fileManager;

        public BuildMonitorFileRepo(IFileManager fileManager,
            IBuildMonitorSettings settings)
        {
            _fileManager = fileManager;
            _settings = settings;
        }

        public MonitorConfiguration GetConfig()
        {
            if (!_fileManager.Exists(_settings.ConfigFile))
            {
                return new MonitorConfiguration();
            }
            return _fileManager.Read(_settings.ConfigFile).FromJsonTo<MonitorConfiguration>();
        }

        public void Save(MonitorConfiguration monitor)
        {
            _fileManager.Write(_settings.ConfigFile, monitor.ToJson(true, true));
        }
    }
}