namespace MasterData.Core.Domain.Products
{
    public class Color : MasterDataObject
    {
        public virtual string Name { get; set; }
        public virtual int Red { get; set; }
        public virtual int Green { get; set; }
        public virtual int Blue { get; set; }
        public virtual decimal Alpha { get; set; }
    }
}