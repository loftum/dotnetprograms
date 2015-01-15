namespace MasterData.Core.Model.Products
{
    public class ColorModel : MasterDataObjectModel
    {
        public string Name { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public decimal Alpha { get; set; }

        public string Rgb
        {
            get { return string.Format("#{0:X02}{1:X02}{2:X02}", Red, Green, Blue); }
        }
    }
}