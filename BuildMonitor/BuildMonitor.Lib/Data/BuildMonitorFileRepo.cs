using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Lib.Data
{
    public class BuildMonitorFileRepo : IBuildMonitorRepo
    {
        private static MonitorConfiguration _config;

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
            return _config ?? (_config = ReadConfig());
        }

        public BuildServerConfig GetBuildServerConfig()
        {
            return GetConfig().BuildServerConfig;
        }

        private MonitorConfiguration ReadConfig()
        {
            if (!_fileManager.Exists(_settings.ConfigFile))
            {
                return new MonitorConfiguration();
            }
            return _fileManager.Read(_settings.ConfigFile).FromJsonTo<MonitorConfiguration>();
        }

        public void Save()
        {
            _fileManager.Write(_settings.ConfigFile, _config.ToJson(true, true));
        }

        public void Revert()
        {
            _config = ReadConfig();
        }
    }
}