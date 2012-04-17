using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Model
{
    public class TraktorModel : BaseModel<Traktor>
    {
        public string Merke { get; set; }
        
        public TraktorModel()
        {
            
        }

        public TraktorModel(Traktor traktor) : base(traktor)
        {
            Merke = traktor.Merke;
        }

        protected override Traktor MapTo(Traktor traktor)
        {
            traktor.Merke = Merke;
            return traktor;
        }
    }
}