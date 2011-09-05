using DbTool.Lib.Configuration;
using DbToolGui.Communication.Commands;
using DbToolGui.Data;

namespace DbToolGui.Communication
{
    public interface IDatabaseCommunicator
    {
        string ConnectedTo { get; }
        bool IsConnected { get; }
        void ConnectTo(ConnectionData connectionData);
        void Disconnect();
        IDbCommandResult Execute(string statement);
        Schema LoadSchema();
    }
}