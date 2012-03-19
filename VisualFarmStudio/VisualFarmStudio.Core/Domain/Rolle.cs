using System.Collections.Generic;

namespace VisualFarmStudio.Core.Domain
{
    public class Rolle : DomainObject
    {
        public virtual string Kode { get; set; }
        public virtual string Navn { get; set; }
        public virtual IList<Bonde> Bondes { get; set; }

        public Rolle()
        {
            Bondes = new List<Bonde>();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Rolle;
            return other != null && other.Kode.Equals(Kode);
        }

        public override int GetHashCode()
        {
            return Kode.GetHashCode();
        }
    }
}