using System;
using DbTool.Lib.Communication;
using DbTool.Lib.Configuration;
using DbTool.Lib.Connections;
using DbTool.Lib.Ui.Syntax;
using DbToolMac.ExtensionMethods;
using DbTool.Lib.ExtensionMethods;
using DbToolMac.Models;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace DbToolMac
{
    public partial class MainWindowController : MonoMac.AppKit.NSWindowController
    {
        public new MainWindow Window { get { return (MainWindow)base.Window; } }

        [Export("connection")]
        public ConnectionViewModel Connection { get; private set; }
        [Export("query")]
        public QueryResultViewModel Query { get; private set; }
        [Export("statusText")]
        public string StatusText { get; set; }
        [Export("title")]
        public string Title {get; set;}

        private readonly IConnectionDataProvider _connectionDataProvider;
        private readonly IDatabaseCommunicator _communicator;
        private readonly IDbToolSettings _settings;
        private readonly ISchemaObjectProvider _schemaObjectProvider;

        // Called when created from unmanaged code
        public MainWindowController(IntPtr handle) : base (handle)
        {
            Console.WriteLine("ctor with Intptr={0}", handle);
            Initialize();
        }
		
        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public MainWindowController(NSCoder coder) : base (coder)
        {
            Console.WriteLine("initWithCoder {0}", coder);
            Initialize();
        }
		
        // Call to load from the XIB/NIB file
        public MainWindowController() : base ("MainWindow")
        {
            Initialize();
        }

        public MainWindowController(IConnectionDataProvider connectionDataProvider,
            IDatabaseCommunicator communicator,
            IDbToolSettings settings,
            ISchemaObjectProvider schemaObjectProvider)
            : base("MainWindow")
        {
            _connectionDataProvider = connectionDataProvider;
            _communicator = communicator;
            _settings = settings;
            _schemaObjectProvider = schemaObjectProvider;
            Initialize();
        }

        void Initialize()
        {
            Connection = new ConnectionViewModel(_connectionDataProvider);
            Query = new QueryResultViewModel();
            StatusText = "Pølse";
            Title = "DbTool";
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            EditorBox.Font = NSFont.FromFontName("Monaco", 12);
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
            StatusText = string.Format("Disconnected from {0}", name);
            Title = "DbTool";
        }

        private void Connect()
        {
            if (Connection.SelectedConnection.IsNullOrEmpty())
            {
                StatusText = "No connection selected";
                return;
            }
            if (_communicator.IsConnected)
            {
                StatusText = string.Format("Already connected to {0}", _communicator.ConnectedTo);
                return;
            }
            var connection = _connectionDataProvider.GetConnection(Connection.SelectedConnection);
            if (connection == null)
            {
                StatusText = string.Format("Invalid connection {0}", Connection.SelectedConnection);
            }
            _communicator.ConnectTo(connection);
            if (_settings.LoadSchema)
            {
                StatusText = "Loading schema objects";
                _schemaObjectProvider.Schema = _communicator.LoadSchema();
            }
            Title = string.Format("DbTool - {0}", _communicator.ConnectedTo);
            StatusText = string.Format("Connected to {0}", _communicator.ConnectedTo);
        }
    }
}

