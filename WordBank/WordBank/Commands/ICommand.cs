namespace Wordbank.Commands
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(CommandArgs args);
        string GetUsageAndExamples();
    }
}