using System.Collections.ObjectModel;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;

namespace DbToolGui.ViewModels
{
    public class ConnectionViewModel : ViewModelBase
    {
        private readonly IDbToolSettings _settings;

        private bool _enableConnectionDropdown = true;
        public bool EnableConnectionDropdown
        {
            get { return _enableConnectionDropdown; }
            set { _enableConnectionDropdown = value; OnPropertyChanged(() => EnableConnectionDropdown); }
        }

        public ObservableCollection<string> AvailableContexts { get; private set; }

        private string _selectedContext;
        public string SelectedContext
        {
            get { return _selectedContext; }
            set
            {
                _selectedContext = value;
                OnPropertyChanged(() => SelectedContext);
                PopulateConnectionsFor(value);
            }
        }

        public ObservableCollection<string> AvailableConnections { get; private set; }

        private string _selectedConnection;
        public string SelectedConnection
        {
            get { return _selectedConnection; }
            set { _selectedConnection = value; OnPropertyChanged(() => SelectedConnection); }
        }

        public ConnectionViewModel(IDbToolSettings settings)
        {
            _settings = settings;
            AvailableContexts = new ObservableCollection<string>();
            AvailableConnections = new ObservableCollection<string>();
            PopulateContexts();
            PopulateConnectionsFor(_settings.CurrentContext);
        }

        private void PopulateContexts()
        {
            _settings.Contexts.Each(context => AvailableContexts.Add(context.Name));
            var currentContext = _settings.CurrentContext;
            SelectedContext = currentContext.Name;
        }

        private void PopulateConnectionsFor(string contextName)
        {
            var context = _settings.Contexts.First(c => c.Name.Equals(contextName));
            PopulateConnectionsFor(context);
        }

        private void PopulateConnectionsFor(DbToolContext context)
        {
            AvailableConnections.Clear();
            context.Databases.Each(connection => AvailableConnections.Add(connection.Name));

            var firstConnection = context.Databases.FirstOrDefault();
            if (firstConnection != null)
            {
                SelectedConnection = firstConnection.Name;
            }
        }

        public void ShowConnected(bool connected)
        {
            EnableConnectionDropdown = !connected;
        }
    }
}