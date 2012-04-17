using System.Collections.Generic;
using System.Linq;
using VisualFarmStudio.Common.ExtensionMethods;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Model
{
    public class StallModel : BaseModel<Stall>
    {
        public IList<HestModel> Hester { get; set; }

        public StallModel()
        {
            Hester = new List<HestModel>();
        }

        public StallModel(Stall stall) : base(stall)
        {
            Hester = stall.Hestes.Select(hest => new HestModel(hest)).ToList();
        }

        protected override Stall MapTo(Stall stall)
        {
            Hester.Each(h => stall.AddHest(h.ToEntity()));
            return stall;
        }
    }
}