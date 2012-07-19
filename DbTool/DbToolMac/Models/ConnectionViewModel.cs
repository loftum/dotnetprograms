using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using System.Linq;
using DbTool.Lib.Connections;
using DbTool.Lib.Configuration;

namespace DbToolMac.Models
{
    public class ConnectionViewModel : ViewModelBase
    {
		[Export("availableContexts")]
		public string[] AvailableContexts { get; set; }

		[Export("selectedContext")]
		public string SelectedContext { get; set; }

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

        public ConnectionViewModel(IDbToolSettings settings)
        {
            if (settings != null)
            {
                AvailableContexts = settings.Contexts.Select(c => c.Name).ToArray();
				var currentContext = settings.CurrentContext;
				SelectedContext = currentContext.Name;

				AvailableConnections = currentContext.Connections.Select(c => c.Name).ToArray();
                SelectedConnection = currentContext.Connections.First().Name;
            }
            ConnectionButtonText = "Connect";
        }

        public void ShowConnected(bool connected)
        {
            ConnectionButtonText = connected ? "Disconnect" : "Connect";
        }
    }
}

