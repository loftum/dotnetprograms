using System.Data.Common;
using DbTool.Lib.Configuration;

namespace DbTool.Lib.Communication.DbCommands.CSharp
{
    public interface ICSharpExecutor : IDbCommandExecutor
    {
        DbToolDatabase Db { set; }
        DbConnection DbConnection { set; }
    }
}