using System.Configuration;

namespace Wordbank.Lib.Config
{
    public class WordBankSettings : IWordBankSettings
    {
        private static WordBankSettings _current;
        public static WordBankSettings Current
        {
            get { return _current ?? (_current = new WordBankSettings()); }
        }

        public string DatabaseProvider
        {
            get { return GetSetting("DatabaseProvider"); }
        }

        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["WordBankDb"].ConnectionString; }
        }

        private static string GetSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}