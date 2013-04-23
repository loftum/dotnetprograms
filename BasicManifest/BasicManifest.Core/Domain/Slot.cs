namespace BasicManifest.Core.Domain
{
    public class Slot : DomainObject
    {
        public virtual decimal Price { get; set; }
        private Skydiver _skydiver;
        public virtual Skydiver Skydiver
        {
            get { return _skydiver; }
            set
            {
                JumperPays = value.IsParticipant;
                _skydiver = value;
            }
        }
        public virtual bool JumperPays { get; set; }

        public Slot()
        {
        }

        public Slot(Skydiver skydiver)
        {
            Skydiver = skydiver;
        }
    }
}