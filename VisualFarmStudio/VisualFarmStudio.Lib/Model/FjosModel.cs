using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Core.Domain;
using VisualFarmStudio.Lib.ExtensionMethods;

namespace VisualFarmStudio.Lib.Model
{
    public class FjosModel : BaseModel<Fjos>
    {
        public IList<KuModel> Kuer { get; set; }

        public FjosModel(Fjos fjos) : base(fjos)
        {
            Kuer = fjos.Kuer.Select(ku => new KuModel(ku)).ToList();
        }

        protected override Fjos MapTo(Fjos fjos)
        {
            Kuer.Each(ku => fjos.AddKu(ku.ToEntity()));
            return fjos;
        }
    }
}