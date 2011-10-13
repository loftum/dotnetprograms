using System;
using DbToolMac.Models;
using DbTool.Lib.Configuration;
using DbTool.Lib.Connections;
using DbTool.Lib.Communication;
using DbTool.Lib.Data;
using DbTool.Lib.Ui.Syntax;

namespace DbToolMac.Delegation
{
    public class DbToolControllerDelegate : IDbToolControllerDelegate
    {
        public MainWindowViewModel Model { get; private set; }

        private readonly IConnectionDataProvider _connectionDataProvider;
        private readonly IDatabaseCommunicator _communicator;
        private readonly IDbToolSettings _settings;
        private readonly ISchemaObjectProvider _schemaObjectProvider;

        public DbToolControllerDelegate(IConnectionDataProvider connectionDataProvider,
            IDatabaseCommunicator communicator,
            IDbToolSettings settings,
            ISchemaObjectProvider schemaObjectProvider)
        {
            _connectionDataProvider = connectionDataProvider;
            _communicator = communicator;
            _settings = settings;
            _schemaObjectProvider = schemaObjectProvider;
            Model = new MainWindowViewModel();
        }
    }
}

