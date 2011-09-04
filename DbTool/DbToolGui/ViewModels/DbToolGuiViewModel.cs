using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DbToolGui.Commands;
using DbToolGui.Connections;
using DbToolGui.Exceptions;
using DbToolGui.ExtensionMethods;
using DbToolGui.Providers;

namespace DbToolGui.ViewModels
{
    public class DbToolGuiViewModel : ViewModelBase
    {
        public string Title
        {
            get
            {
                return _communicator.IsConnected
                           ? string.Format("DbTool - {0}", _communicator.ConnectedTo)
                           : "DbTool";
            }
        }

        public string Icon
        {
            get { return _communicator.IsConnected ? "/Images/dbplus.ico" : "/Images/db.ico"; }
        }

        private readonly IConnectionProvider _connectionProvider;
        private readonly IDatabaseCommunicator _communicator;

        public ICommand ExecuteCommand { get; private set; }
        public string ConnectCommandName
        {
            get { return _communicator.IsConnected ? "Disconnect" : "Connect"; }
        }
        public ICommand ConnectCommand { get; private set; }

        private string _statusText;
        public string StatusText
        {
            get { return _statusText; }
            set { _statusText = value; OnPropertiesChanged("StatusText"); }
        }

        public string EditorText { get; set; }

        private string _resultText;
        public string ResultText
        {
            get { return _resultText; }
            set { _resultText = value; OnPropertyChanged("ResultText"); }
        }

        public QueryResultViewModel QueryResult { get; private set; }
        public ObservableCollection<string> AvailableConnections { get; private set; }
        
        private string _selectedConnection;
        public string SelectedConnection
        {
            get { return _selectedConnection; }
            set { _selectedConnection = value; OnPropertiesChanged("SelectedConnection"); }
        }

        public DbToolGuiViewModel(IConnectionProvider connectionProvider, IDatabaseCommunicator communicator)
        {
            _connectionProvider = connectionProvider;
            _communicator = communicator;

            ConnectCommand = new DelegateCommand(ToggleConnect);
            ExecuteCommand = new DelegateCommand(ExecuteStatement);
            AvailableConnections = new ObservableCollection<string>();
            foreach (var connection in _connectionProvider.GetConnectionNames())
            {
                AvailableConnections.Add(connection);
            }
            SelectedConnection = _connectionProvider.GetDefaultConnectionName();
            QueryResult = new QueryResultViewModel();
        }

        private void ToggleConnect(object arg)
        {
            if (_communicator.IsConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
        }

        private void Disconnect()
        {
            var name = _communicator.ConnectedTo;
            _communicator.Disconnect();
            StatusText = string.Format("Disconnected from {0}", name);
            FireOnConnectionPropertiesChanged();
        }

        private void Connect()
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
            StatusText = string.Format("Connected to {0}", _communicator.ConnectedTo);
            FireOnConnectionPropertiesChanged();
        }

        private void FireOnConnectionPropertiesChanged()
        {
            OnPropertiesChanged("ConnectCommandName", "Icon", "Title");
        }

        private void ExecuteStatement(object arg)
        {
            var statement = EditorText.Trim();
            if (statement.IsNullOrEmpty())
            {
                return;
            }
            try
            {
                QueryResult.Clear();
                var result = _communicator.Execute(statement);
                if (result is QueryResult)
                {
                    QueryResult.Show((QueryResult) result);
                }
                else
                {
                    ResultText = result.ToString();
                }
            }
            catch(UserException ex)
            {
                ResultText = ex.Message;
            }
            catch (Exception ex)
            {
                ResultText = ex.ToString();
            }
        }
    }
}