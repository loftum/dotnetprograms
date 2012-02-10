using Wordbank.Lib.Domain;
using Wordbank.Lib.DomainMapping;

namespace WordBank.Lib.DomainMapping
{
    public class OrdMap : DomainObjectMap<Ord>
    {
        public OrdMap()
        {
            Map(o => o.IdentifikasjonsNummer);
            Map(o => o.Grunnform);
            Map(o => o.Fullform);
            Map(o => o.MorfologiskBeskrivelse);
            Map(o => o.ParadigmeKode);
            Map(o => o.Nummer);
            HasManyToMany(o => o.Paradigmes).Table("OrdParadigme");
        }
    }
}