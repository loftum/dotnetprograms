using System;
using System.Collections.Generic;
using MonoMac.Foundation;
using System.Linq;

namespace DbToolMac.Models
{
    public class MainWindowViewModel : NSObject
    {
        [Export("availableConnections")]
        public string[] AvailableConnections { get; set; }

        [Export("selectedConnection")]
        public string SelectedConnection { get; set; }

        public MainWindowViewModel()
        {
            AvailableConnections = new [] {"MySql", "SqlServer"};
            SelectedConnection = AvailableConnections.First();
        }
    }
}

