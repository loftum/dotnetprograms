namespace VisualFarmStudio.Core.Domain
{
    public class Hest : Dyr
    {
        public virtual Stall Stall { get; set; }

        public virtual string Knegg()
        {
            return "Knegg";
        }
    }
}