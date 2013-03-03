using System;
using System.Linq;
using DbTool.Lib.Communication;
using DbTool.Lib.Configuration;
using DbTool.Lib.Connections;
using DbTool.Lib.Syntax;
using DbToolMac.ExtensionMethods;
using DbTool.Lib.ExtensionMethods;
using DbToolMac.Models;
using MonoMac.AppKit;
using MonoMac.Foundation;
using DbTool.Lib.Ui.ModelBinding;
using DbTool.Lib.Communication.DbCommands;
using DbTool.Lib.Meta;
using DbTool.Lib.Communication.DbCommands.Results;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbToolMac
{
    public partial class MainWindowController : NSWindowController
    {
        public new MainWindow Window { get { return (MainWindow)base.Window; } }

        [Export("model")]
        public MainWindowViewModel Model {get; private set;}
        [Export("connection")]
        public ConnectionViewModel Connection { get; private set; }
        [Export("query")]
        public QueryResultViewModel Query { get; private set; }

        private readonly IDatabaseCommunicator _communicator;
        private readonly IDbToolSettings _settings;
        private readonly ITypeCache _typeCache;

        // Called when created from unmanaged code
        public MainWindowController(IntPtr handle) : base (handle)
        {
            Initialize();
        }
		
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base (coder)
        {
            Initialize();
        }
		
        // Call to load from the XIB/NIB file
        public MainWindowController() : base ("MainWindow")
        {
            Initialize();
        }

        public MainWindowController(IDatabaseCommunicator communicator,
            IDbToolSettings settings,
            ITypeCache typeCache)
            : base("MainWindow")
        {
            _communicator = communicator;
            _settings = settings;
            _typeCache = typeCache;
            Initialize();
        }

        void Initialize()
        {
            Model = new MainWindowViewModel();
            Connection = new ConnectionViewModel(_settings);
            Query = new QueryResultViewModel();
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();

            new ModelBinder<MainWindowViewModel>(Model)
                .Bind(model => model.StatusText, () => StatusField.StringValue = Model.StatusText)
                .Bind(model => model.Title, () => Window.Title = Model.Title);
            new ModelBinder<ConnectionViewModel>(Connection)
				.Bind(model => model.ConnectionButtonText, () => ConnectionButton.Title = Connection.ConnectionButtonText);


        }

        public override void KeyUp(NSEvent e)
        {
            if (e.KeyCode == 96)
            {
				var command = EditorBox.GetSelectedOrAllText();
				Model.StatusText = command;
                _communicator.StartExecute(command, QueryFinished);
            }
        }

        private void QueryFinished(IDbCommandResult result)
        {
            Model.StatusText = result.ToString ();
        }

        partial void Connection_Click(NSObject sender)
        {
            ToggleConnect();
            var statement = EditorBox.GetSelectedOrAllText();
            ResultTextBox.SetText(statement);
        }
 
        private void ToggleConnect()
        {
            if (_communicator.IsConnected)
            {
                Disconnect();
            }
            else
            {
                Connect();
            }
            Connection.ShowConnected(_communicator.IsConnected);
        }

        private void Disconnect()
        {
            var name = _communicator.ConnectedTo;
            _communicator.Disconnect();
            Model.StatusText = string.Format("Disconnected from {0}", name);
            Model.Title = "DbTool";
            Connection.ShowConnected(true);
        }

        private void Connect()
        {
            if (Connection.SelectedConnection.IsNullOrEmpty())
            {
                Model.StatusText = "No connection selected";
                return;
            }
            if (_communicator.IsConnected)
            {
                Model.StatusText = string.Format("Already connected to {0}", _communicator.ConnectedTo);
                return;
            }

			var database = _settings.CurrentContext.GetDatabase(Connection.SelectedConnection);
            if (database == null)
            {
                Model.StatusText = string.Format("Invalid connection {0}", Connection.SelectedConnection);
            }

            _communicator.ConnectTo(database);
            if (_settings.LoadSchema)
            {
                Model.StatusText = "Loading schema objects";
                _typeCache.Schema = _communicator.LoadSchema();
            }
            Model.Title = string.Format("DbTool - {0}", _communicator.ConnectedTo);
            Model.StatusText = string.Format("Connected to {0}", _communicator.ConnectedTo);
            Connection.ShowConnected(true);
        }
    }
}

