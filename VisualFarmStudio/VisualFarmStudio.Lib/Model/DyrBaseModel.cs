using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Model
{
    public abstract class DyrBaseModel<TDyr> : BaseModel<TDyr> where TDyr : Dyr, new()
    {
        public string Navn { get; set; }

        protected DyrBaseModel(TDyr dyr) : base(dyr)
        {
            Navn = dyr.Navn;
        }

        protected override TDyr MapTo(TDyr dyr)
        {
            dyr.Navn = Navn;
            return dyr;
        }
    }
}