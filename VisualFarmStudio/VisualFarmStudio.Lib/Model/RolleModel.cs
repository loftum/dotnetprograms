using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Model
{
    public class RolleModel : BaseModel<Rolle>
    {
        public string Kode { get; set; }
        public string Navn { get; set; }

        public RolleModel(Rolle rolle) : base(rolle)
        {
            Kode = rolle.Kode;
            Navn = rolle.Navn;
        }

        public RolleModel()
        {
        }

        protected override Rolle MapTo(Rolle rolle)
        {
            rolle.Kode = Kode;
            rolle.Navn = Navn;
            return rolle;
        }
    }
}