namespace MovieBase.Domain
{
    public class DomainObject
    {
        public virtual long Id { get; set; }
        public virtual bool IsPersisted
        {
            get { return Id > 0; }
        }
    }
}