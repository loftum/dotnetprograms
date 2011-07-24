using System;

namespace StuffLibrary.UnitTesting.Builders
{
    public class BuilderBase<TItem>
    {
        public TItem Item { get; protected set; }

        private static long _currentId;
        private static long _currentVersion;

        protected static long NextId()
        {
            return ++_currentId;
        }

        protected static byte[] NextVersion()
        {
            return BitConverter.GetBytes(++_currentVersion);
        }
    }
}