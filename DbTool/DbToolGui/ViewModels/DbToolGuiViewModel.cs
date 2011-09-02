using System.Collections.ObjectModel;
using System.Windows.Input;
using DbToolGui.Commands;
using DbToolGui.Connections;
using DbToolGui.Providers;

namespace DbToolGui.ViewModels
{
    public class DbToolGuiViewModel : ViewModelBase
    {
        private readonly ConnectionProvider _connectionProvider = new ConnectionProvider();
        private readonly DatabaseCommunicator _communicator = new DatabaseCommunicator();

        public ICommand ExecuteCommand { get; private set; }
        public ICommand ConnectCommand { get; private set; }

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set { _statusText = value; OnPropertiesChanged("StatusText"); }
        }

        private string _editorText;
        public string EditorText
        {
            get { return _editorText; }
            set { _editorText = value; OnPropertyChanged("EditorText"); }
        }

        public ObservableCollection<string> AvailableConnections { get; private set; }
        
        private string _selectedConnection;
        public string SelectedConnection
        {
            get { return _selectedConnection; }
            set { _selectedConnection = value; OnPropertiesChanged("SelectedConnection"); }
        }

        public DbToolGuiViewModel()
        {
            ConnectCommand = new DelegateCommand(Connect);
            ExecuteCommand = new DelegateCommand(ExecuteQuery);
            AvailableConnections = new ObservableCollection<string>();
            foreach (var connection in _connectionProvider.GetConnectionNames())
            {
                AvailableConnections.Add(connection);
            }
            SelectedConnection = _connectionProvider.GetDefaultConnectionName();
        }

        private void Connect(object obj)
        {
            if (string.IsNullOrEmpty(_selectedConnection))
            {
                StatusText = "No connection selected";
                return;
            }
            if (_communicator.IsConnected)
            {
                StatusText = string.Format("Already conneced to {0}", _communicator.ConnectedTo);
                return;
            }
            var connection = _connectionProvider.GetConnection(SelectedConnection);
            if (connection == null)
            {
                StatusText = string.Format("Invalid connection {0}", SelectedConnection);
                return;
            }
            _communicator.ConnectTo(connection);
            StatusText = string.Format("Connected to {0}", SelectedConnection);
        }

        private void ExecuteQuery(object arg)
        {

        }
    }
}