using System;
using StructureMap.Pipeline;

namespace BasicManifest.Web.Ioc
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