using System;
using System.Threading;
using System.Windows.Input;
using System.Windows.Threading;
using DbTool.Lib.Communication;
using DbTool.Lib.Communication.DbCommands.Results;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Memory;
using DbTool.Lib.Objects;
using DbTool.Lib.Objects.Database;
using DbTool.Lib.Ui.Syntax;
using DbToolGui.Commands;

namespace DbToolGui.ViewModels
{
    public class MainViewModel : ViewModelBase
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

        private readonly MemoryMeter _memoryMeter;
        private string _memoryUsage;
        public string MemoryUsage
        {
            get { return _memoryUsage; }
            set { _memoryUsage = value; OnPropertyChanged(() => MemoryUsage); }
        }

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

        private readonly Dispatcher _dispatcher;
        private readonly IDbToolSettings _settings;
        private readonly IObjectCache _objectCache;

        public MainViewModel(IDatabaseCommunicator communicator,
            IDbToolSettings settings,
            IObjectCache objectCache)
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            _communicator = communicator;
            _settings = settings;
            _objectCache = objectCache;

            ConnectCommand = new DelegateCommand(ToggleConnect);
            ExecuteCommand = new DelegateCommand(ExecuteStatement);
            
            Connection = new ConnectionViewModel(_settings);
            QueryResult = new QueryResultViewModel();
            _memoryMeter = new MemoryMeter(mem => MemoryUsage = mem.ToMemoryUsage());
            _memoryMeter.Start();
        }

        private void ToggleConnect(object arg)
        {
            if (_communicator.IsConnected)
            {
                Disconnect();
                Connection.ShowConnected(false);
            }
            else
            {
                Connect();
                Connection.ShowConnected(true);
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
            var databaseName = Connection.SelectedConnection;
            if (string.IsNullOrEmpty(databaseName))
            {
                StatusText = "No connection selected";
                return;
            }
            if (_communicator.IsConnected)
            {
                StatusText = string.Format("Already conneced to {0}", _communicator.ConnectedTo);
                return;
            }
            var database = _settings.CurrentContext.GetDatabase(databaseName);
            if (database == null)
            {
                StatusText = string.Format("Invalid connection {0}", Connection.SelectedConnection);
                return;
            }

            _communicator.ConnectTo(database);
            
            FireOnConnectionPropertiesChanged();
            if (_settings.LoadSchema)
            {
                StatusText = "Loading schema objects";
                _objectCache.Schema = _communicator.LoadSchema();
            }
            StatusText = string.Format("Connected to {0}", _communicator.ConnectedTo);
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
                new Thread(() => _communicator.StartExecute(statement, ResultReady)).Start();
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

        private void ResultReady(IDbCommandResult result)
        {
            _dispatcher.Invoke(DispatcherPriority.Normal, new Action<IDbCommandResult>(ShowResult), result);
        }

        private void ShowResult(IDbCommandResult result)
        {
            QueryResult.Show(result);
            StatusText = "Done";
        }
    }
}