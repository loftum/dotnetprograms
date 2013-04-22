namespace BasicManifest.Core.Domain
{
    public class Slot : DomainObject
    {
        public virtual decimal Price { get; set; }
        private Person _jumper;
        public virtual Person Jumper
        {
            get { return _jumper; }
            set
            {
                JumperPays = value.IsParticipant;
                _jumper = value;
            }
        }
        public virtual bool JumperPays { get; set; }

        public Slot()
        {
        }

        public Slot(Person jumper)
        {
            Jumper = jumper;
        }
    }
}