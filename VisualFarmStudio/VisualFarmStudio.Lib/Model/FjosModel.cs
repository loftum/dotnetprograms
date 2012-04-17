using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Common.ExtensionMethods;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Model
{
    public class FjosModel : BaseModel<Fjos>
    {
        public IList<KuModel> Kuer { get; set; }

        public FjosModel()
        {
            Kuer = new List<KuModel>();
        }

        public FjosModel(Fjos fjos) : base(fjos)
        {
            Kuer = fjos.Kues.Select(ku => new KuModel(ku)).ToList();
        }

        protected override Fjos MapTo(Fjos fjos)
        {
            Kuer.Each(ku => fjos.AddKu(ku.ToEntity()));
            return fjos;
        }
    }
}