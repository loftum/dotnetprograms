using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using MongoTool.Annotations;
using MongoTool.Core.CSharp;

namespace MongoTool.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string Input { get; set; }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                AfterAwhile(() => StatusText = null);
                OnPropertyChanged();
            }
        }

        private static void AfterAwhile(Action action)
        {
            new Thread(() =>
            {
                Thread.Sleep(7000);
                action();
            }).Start();
        }

        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public ICommand ExecuteCommand { get; private set; }

        private readonly ICSharpEvaluator _evaluator;
        private string _result;
        private string _statusText;

        public MainViewModel(ICSharpEvaluator evaluator)
        {
            _evaluator = evaluator;
            ExecuteCommand = new DelegateCommand(ExecuteStatement);
        }

        private void ExecuteStatement(object arg)
        {
            var statement = Input.Trim();
            if (string.IsNullOrEmpty(statement))
            {
                return;
            }
            var result = _evaluator.Execute(statement);
            Result = result.ToString();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}