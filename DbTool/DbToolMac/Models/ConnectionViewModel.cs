using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using System.Linq;
using DbTool.Lib.Connections;

namespace DbToolMac.Models
{
    public class ConnectionViewModel : ViewModelBase
    {
        [Export("availableConnections")]
        public string[] AvailableConnections { get; set; }
        [Export("selectedConnection")]
        public string SelectedConnection { get; set; }

        private string _connectionButtonText;
        [Export("connectionButtonText")]
        public string ConnectionButtonText
        {
            get { return _connectionButtonText; }
            set { _connectionButtonText = value; OnPropertyChange(() => ConnectionButtonText);}
        }

        private readonly IConnectionDataProvider _connectionDataProvider;

        public ConnectionViewModel(IConnectionDataProvider connectionDataProvider)
        {
            if (connectionDataProvider != null)
            {
                _connectionDataProvider = connectionDataProvider;
                AvailableConnections = _connectionDataProvider
                    .GetConnectionNames()
                    .ToArray();
                SelectedConnection = _connectionDataProvider.GetDefaultConnectionName();
            }
            ConnectionButtonText = "Connect";
        }

        public void ShowConnected(bool connected)
        {
            ConnectionButtonText = connected ? "Disconnect" : "Connect";
        }
    }
}

