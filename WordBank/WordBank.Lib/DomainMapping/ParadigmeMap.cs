using Wordbank.Lib.Domain;
using Wordbank.Lib.DomainMapping;

namespace WordBank.Lib.DomainMapping
{
    public class ParadigmeMap : DomainObjectMap<Paradigme>
    {
        public ParadigmeMap()
        {
            Map(p => p.Kode);
            Map(p => p.Ordklasse);
            Map(p => p.Beskrivelse);
            Map(p => p.Fullstendig);
            Map(p => p.Eksempel);
            Map(p => p.Nummer);
            Map(p => p.MorfologiskBeskrivelse);
            Map(p => p.Endelser);
            HasMany(p => p.Ords);
        }
    }
}