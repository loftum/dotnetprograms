namespace VisualFarmStudio.Core.Domain
{
    public class Traktor : DomainObject
    {
        public virtual Bondegard Bondegard { get; set; }
        public virtual string Merke { get; set; }
    }
}