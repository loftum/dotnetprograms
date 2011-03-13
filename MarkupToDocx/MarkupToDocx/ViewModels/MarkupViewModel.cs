using System;
using System.Diagnostics;
using System.Windows.Input;
using MarkupToDocx.Commands;
using MarkupToDocx.Lib.Conversion;

namespace MarkupToDocx.ViewModels
{
    public class MarkupViewModel : ViewModelBase
    {
        private readonly IMarkupConverter _markupConverter;
        public ICommand OpenInWordCommand { get; private set; }

        private string _markupText;
        public string MarkupText
        {
            get { return _markupText; }
            set { _markupText = value; OnPropertiesChanged("MarkupText"); }
        }

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set { _statusText = value; OnPropertyChanged("StatusText");
                NewStatusText = true;
            }
        }

        private bool _newStatusText;
        public bool NewStatusText
        {
            get { return _newStatusText; }
            set { _newStatusText = value; OnPropertyChanged("NewStatusText"); }
        }
        

        public MarkupViewModel(IMarkupConverter markupConverter)
        {
            _markupConverter = markupConverter;
            OpenInWordCommand = new DelegateCommand(OpenInWord);
        }

        private void OpenInWord(object obj)
        {
            try
            {
                var markup = MarkupText ?? string.Empty;
                var path = _markupConverter.Convert(markup);
                Process.Start(path);
            }
            catch (Exception e)
            {
                StatusText = e.ToString();
            }
        }
    }
}