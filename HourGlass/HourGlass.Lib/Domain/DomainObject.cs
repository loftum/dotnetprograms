namespace HourGlass.Lib.Domain
{
    public abstract class DomainObject
    {
        public virtual long Id { get; set; }
        public virtual bool IsPersisted { get { return Id > 0; } }
    }
}