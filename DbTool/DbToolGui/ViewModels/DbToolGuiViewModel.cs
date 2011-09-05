using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;
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

        public ConnectionViewModel Connection { get; private set; }
        public QueryResultViewModel QueryResult { get; private set; }

        public DbToolGuiViewModel(IConnectionProvider connectionProvider, IDatabaseCommunicator communicator)
        {
            _connectionProvider = connectionProvider;
            _communicator = communicator;

            ConnectCommand = new DelegateCommand(ToggleConnect);
            ExecuteCommand = new DelegateCommand(ExecuteStatement);
            
            Connection = new ConnectionViewModel(_connectionProvider);
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
            if (string.IsNullOrEmpty(Connection.SelectedConnection))
            {
                StatusText = "No connection selected";
                return;
            }
            if (_communicator.IsConnected)
            {
                StatusText = string.Format("Already conneced to {0}", _communicator.ConnectedTo);
                return;
            }
            var connection = _connectionProvider.GetConnection(Connection.SelectedConnection);
            if (connection == null)
            {
                StatusText = string.Format("Invalid connection {0}", Connection.SelectedConnection);
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
                StatusText = "Executing";
                var result = _communicator.Execute(statement);
                QueryResult.Show(result);
                StatusText = "Done";
            }
            catch(UserException ex)
            {
                QueryResult.Show(ex.Message);
            }
            catch (Exception ex)
            {
                QueryResult.Show(ex.ToString());
            }
        }
    }
}