using System.Collections.Generic;

namespace Wordbank.Lib.Domain
{
    public class Paradigme : DomainObject
    {
        //        Paradigmene er noe ulikt beskrevet mht kolonne tre, fire og fem. Paradigmene for bokmål 
        //        er gjennomgått og korrigert, mens disse kolonnene for nynorsk foreløpig kan inneholde feil. 

        //        Første kolonne:  paradigmekode
        public virtual string Kode { get; set; }
        //        Andre kolonne:   ordklasse + eventuelt del av morfologisk beskrivelse
        public virtual string Ordklasse { get; set; }
        //        Tredje kolonne:  eventuell beskrivelse av paradigme
        public virtual string Beskrivelse { get; set; }
        //        Fjerde kolonne:  om bøyingsparadigmet er fullstendig eller f.eks bare har entall eller flertall
        public virtual string Fullstendig { get; set; }
        //        Femte kolonne:   eksempel på ord med paradigmet
        public virtual string Eksempel { get; set; }
        //        Sjette kolonne:  nummer i bøyingsparadigme
        public virtual int Nummer { get; set; }
        //        Sjuende kolonne: morfologisk beskrivelse
        public virtual string MorfologiskBeskrivelse { get; set; }
        //        Åttende kolonne: faktisk bøying/bøyingsendelse som tillegges stammen av ordet ved bøying
        public virtual string Endelser { get; set; }

        public virtual IList<Ord> Ords { get; set; }
    }
}