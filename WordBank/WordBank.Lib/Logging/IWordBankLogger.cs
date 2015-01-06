using System;

namespace Wordbank.Lib.Logging
{
    public interface IWordBankLogger
    {
        void Info(string message, params object[] args);
        void Warn(string message, params object[] args);
        void Error(string message, params object[] args);
        void Error(Exception exception, string message, params object[] args);
    }
}