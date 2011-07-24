using System;
using log4net;

namespace StuffLibrary.Common.Logging
{
    public class StuffLibraryLogger : IStuffLibraryLogger
    {
        private readonly ILog _log;

        public StuffLibraryLogger(object owner)
        {
            _log = LogManager.GetLogger(owner.GetType());
        } 

        public StuffLibraryLogger(string name)
        {
            _log = LogManager.GetLogger(name);
        }

        public StuffLibraryLogger(Type type)
        {
            _log = LogManager.GetLogger(type);
        }

        public void Info(object message)
        {
            _log.Info(message);
        }

        public void Debug(object message)
        {
            _log.Debug(message);
        }

        public void Error(object message)
        {
            _log.Error(message);
        }
    }
}