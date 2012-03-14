namespace VisualFarmStudio.Core.Domain
{
    public class DomainObject
    {
        public virtual bool IsNew { get { return Id == 0; } }
        public virtual long Id { get; set; }
    }
}