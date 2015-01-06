using System;
using System.Windows.Input;

namespace MongoTool.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;

        public DelegateCommand(Action<object> execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}