using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Mappings
{
    public class ColorMap : MasterDataObjectMap<Color>
    {
        public ColorMap()
        {
            Map(c => c.Name).Unique();
            Map(c => c.Red);
            Map(c => c.Green);
            Map(c => c.Blue);
            Map(c => c.Alpha);
        }
    }
}