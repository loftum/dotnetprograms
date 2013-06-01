using System.Reflection;

namespace DotNetPrograms.Common.Meta
{
    public class EventMeta : MemberMeta
    {
        private readonly EventInfo _event;

        public override string Name { get { return _event.Name; } }

        public EventMeta(EventInfo theEvent)
        {
            _event = theEvent;
        }

        public bool IsProxiable
        {
            get { return IsProxiableMethod(_event.GetAddMethod()); }
        }

        public bool IsVirtual
        {
            get { return _event.GetAddMethod().IsVirtual; }
        }

        public bool IsFinal
        {
            get { return _event.GetAddMethod().IsFinal; }
        }
    }
}