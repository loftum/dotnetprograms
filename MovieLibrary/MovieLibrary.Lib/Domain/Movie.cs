namespace MovieLibrary.Lib.Domain
{
    public class Movie : DomainObject
    {
        public virtual string Title { get; set; }
        public virtual string SubTitle { get; set; }
    }
}