using System.Collections.Generic;

namespace Wordbank.Lib.Domain
{
    public class Ord : DomainObject
    {
        public virtual long IdentifikasjonsNummer { get; set; }
//        Andre kolonne:  grunnform av ordet
        public virtual string Grunnform { get; set; }
//        Tredje kolonne: fullform av ordet
        public virtual string Fullform { get; set; }
//        Fjerde kolonne: morfologisk beskrivelse
        public virtual string MorfologiskBeskrivelse { get; set; }
//        Femte kolonne:  paradigmekode
        public virtual string ParadigmeKode { get; set; }
//        Sjette kolonne: nummer i paradigme
        public virtual int Nummer { get; set; }

        public virtual IList<Paradigme> Paradigmes { get; set; }
//
//        Fullformen i kolonne tre er generert på grunnlag av paradigmekoden i kolonne fem og 
//        nummer i paradigme i kolonne seks. Den morfologiske beskrivelsen i kolonne fire 
//        kommer også fra paradigmekoden. Paradigmekodene finnes i fila paradigme_bm.txt.
    }
}