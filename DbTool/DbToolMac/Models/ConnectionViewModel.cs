using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using System.Linq;
using DbTool.Lib.Connections;

namespace DbToolMac.Models
{
    public class ConnectionViewModel : NSObject
    {
        [Export("availableConnections")]
        public string[] AvailableConnections { get; set; }
        [Export("selectedConnection")]
        public string SelectedConnection { get; set; }
        [Export("connectionButtonText")]
        public string ConnectionButtonText { get; set; }

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
            ConnectionButtonText = "Pølse";
        }

        public void ShowConnected(bool connected)
        {
            ConnectionButtonText = connected ? "Disconnect" : "Connect";
        }
    }
}

