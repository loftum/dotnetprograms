using System;
using MonoMac.Foundation;

namespace DbToolMac
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _statusText;
        [Export("statusText")]
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                _statusText = value;
                OnPropertyChange(() => StatusText);
            }
        }

        private string _title;
        [Export("title")]
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChange(() => Title);
            }
        }

        public MainWindowViewModel()
        {
            StatusText = "Pølse";
            Title = "DbTool";
        }
    }
}

