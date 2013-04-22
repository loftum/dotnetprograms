using NHibernate.Event;

namespace BasicManifest.Data.Setup
{
    public interface IAuditEventListener : IPreInsertEventListener, IPreUpdateEventListener
    {
    }
}