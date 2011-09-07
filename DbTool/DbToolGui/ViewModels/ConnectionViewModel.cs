using System.Collections.ObjectModel;
using DbToolGui.Providers;

namespace DbToolGui.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly IConnectionProvider _connectionProvider;

        private bool _enableConnectionDropdown = true;
        public bool EnableConnectionDropdown
        {
            get { return _enableConnectionDropdown; }
            set { _enableConnectionDropdown = value; OnPropertyChanged("EnableConnectionDropdown"); }
        }

        public ObservableCollection<string> AvailableConnections { get; private set; }

        private string _selectedConnection;
        public string SelectedConnection
        {
            get { return _selectedConnection; }
            set { _selectedConnection = value; OnPropertiesChanged("SelectedConnection"); }
        }

        public ConnectionViewModel(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
            AvailableConnections = new ObservableCollection<string>();
            foreach (var connection in _connectionProvider.GetConnectionNames())
            {
                AvailableConnections.Add(connection);
            }
            SelectedConnection = _connectionProvider.GetDefaultConnectionName();
        }

        public void ShowConnected(bool connected)
        {
            EnableConnectionDropdown = !connected;
        }
    }
}