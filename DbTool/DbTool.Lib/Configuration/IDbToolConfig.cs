using System.Collections.Generic;

namespace DbTool.Lib.Configuration
{
    public interface IDbToolConfig
    {
        DbToolSettings Settings { get; }
        string SettingsPath { get; }
        IDictionary<string, string> AssemblyMap { get; }
        void SaveSettings();
    }
}