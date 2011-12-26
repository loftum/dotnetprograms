using DbTool.Lib.Communication.DbCommands;
using DbTool.Lib.Configuration;
using DbTool.Lib.Data;

namespace DbTool.Lib.Communication
{
    public interface IDatabaseCommunicator
    {
        void StartExecute(string statement, DatabaseCommunicator.ResultCallback callback);
        string ConnectedTo { get; }
        bool IsConnected { get; }
        void ConnectTo(DbToolDatabase connectionData);
        void Disconnect();
        IDbCommandResult Execute(string statement);
        Schema LoadSchema();
    }
}