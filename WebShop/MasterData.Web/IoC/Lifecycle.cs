using System;
using StructureMap.Pipeline;

namespace MasterData.Web.IoC
{
    public class Lifecycle
    {
        private static Func<ILifecycle> _current;

        public static ILifecycle Current
        {
            get { return _current(); }
            set { _current = () => value; }
        }
    }
}