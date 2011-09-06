namespace DbTool.Lib.Configuration
{
    public interface IDbToolConfig
    {
        DbToolSettings Settings { get; }
        string SettingsPath { get; }
        void SaveSettings();
    }
}