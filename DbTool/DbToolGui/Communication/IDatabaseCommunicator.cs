using DbTool.Lib.Configuration;
using DbToolGui.Communication.Commands;
using DbToolGui.Data;

namespace DbToolGui.Communication
{
    public interface IDatabaseCommunicator
    {
        void StartExecute(string statement, DatabaseCommunicator.ResultCallback callback);
        string ConnectedTo { get; }
        bool IsConnected { get; }
        void ConnectTo(ConnectionData connectionData);
        void Disconnect();
        IDbCommandResult Execute(string statement);
        Schema LoadSchema();
    }
}