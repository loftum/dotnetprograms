namespace Wordbank.Commands
{
    public interface ICommandExecutor
    {
        void Execute(string[] args);
    }
}