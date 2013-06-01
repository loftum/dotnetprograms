namespace DotNetPrograms.Common.UnitTests.Common.Mapping
{
    public class OrderLineDto
    {
        public string ProductId { get; set; }
        public string Description { get; set; }
        public PriceDto Price { get; set; }
    }
}