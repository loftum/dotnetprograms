using System.Collections.ObjectModel;
using DbTool.Lib.Connections;

namespace DbToolGui.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly IConnectionDataProvider _connectionDataProvider;

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

        public ConnectionViewModel(IConnectionDataProvider connectionDataProvider)
        {
            _connectionDataProvider = connectionDataProvider;
            AvailableConnections = new ObservableCollection<string>();
            foreach (var connection in _connectionDataProvider.GetConnectionNames())
            {
                AvailableConnections.Add(connection);
            }
            SelectedConnection = _connectionDataProvider.GetDefaultConnectionName();
        }

        public void ShowConnected(bool connected)
        {
            EnableConnectionDropdown = !connected;
        }
    }
}