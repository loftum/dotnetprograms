using MasterData.Core.Domain.MasterData;

namespace MasterData.UnitTesting.TestData.Builders
{
    public class SaleProductBuilder : ProductBuilder<SaleProduct, SaleProductBuilder>
    {
        public SaleProductBuilder(bool generateId = true)
            : base(new SaleProduct(), generateId)
        {
        }

        public SaleProductBuilder(SaleProduct item, bool generateId = true) : base(item, generateId)
        {
        }
    }
}