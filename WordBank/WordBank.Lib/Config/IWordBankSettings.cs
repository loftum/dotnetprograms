namespace Wordbank.Lib.Config
{
    public interface IWordBankSettings
    {
        string DatabaseProvider { get; }
        string ConnectionString { get; }
    }
}