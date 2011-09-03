using DbTool.Lib.Configuration;

namespace DbToolGui.Connections
{
    public interface IDatabaseCommunicator
    {
        string ConnectedTo { get; }
        bool IsConnected { get; }
        void ConnectTo(ConnectionData connectionData);
        void Disconnect();
        IDbCommandResult Execute(string statement);
    }
}