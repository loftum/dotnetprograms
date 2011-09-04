using System.Collections.ObjectModel;
using DbToolGui.Providers;

namespace DbToolGui.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly IConnectionProvider _connectionProvider;

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
    }
}