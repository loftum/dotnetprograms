using System.Collections.Generic;
using System.ComponentModel;

namespace DeployWizard.Wpf.Steps.Views
{
    public class AutoCompleteSuggestions : INotifyPropertyChanged
    {
        private string _selected;
        private IEnumerable<string> _suggestions;

        public IEnumerable<string> Suggestions
        {
            get
            {
                return _suggestions;
            }
            set
            {
                _suggestions = value;
                OnPropertyChanged("Suggestions");
            }
        }
        
        public string Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (_selected.Equals(value))
                {
                    return;
                }
                _selected = value;
                OnPropertyChanged("Selected");
            }
        }

        public AutoCompleteSuggestions() : this(new string[0])
        {
        }

        public AutoCompleteSuggestions(IEnumerable<string> suggestions)
        {
            Suggestions = suggestions;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
