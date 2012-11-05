using DotNetPrograms.Common.Configuration;

namespace StuffLibrary.Common.Configuration
{
    public class StuffLibrarySettings : AppSettingsBase, IStuffLibrarySettings
    {
        public string RottenTomatoesApiKey { get { return GetAppSettingOrThrow(() => RottenTomatoesApiKey); } }
    }
}