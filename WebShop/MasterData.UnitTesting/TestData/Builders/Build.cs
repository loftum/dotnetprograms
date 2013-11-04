
using MasterData.Core.Domain.Products;

namespace MasterData.UnitTesting.TestData.Builders
{
    public static class Build
    {
        public static StoreProductBuilder StoreProduct()
        {
            return new StoreProductBuilder(new StoreProduct(SupplierProduct()));
        }

        public static Builder<T> A<T>() where T : class, new()
        {
            return new Builder<T>(new T());
        }

        public static Builder<T> A<T>(T item)
        {
            return new Builder<T>(item);
        }

        public static ResellerBuilder Reseller()
        {
            return new ResellerBuilder();
        }

        public static SalespointBuilder Salespoint()
        {
            return new SalespointBuilder();
        }

        public static ProductMasterBuilder ProductMaster()
        {
            return new ProductMasterBuilder();
        }

        public static ProductVariantBuilder ProductVariant()
        {
            return new ProductVariantBuilder(new ProductVariant(ProductMaster()));
        }

        public static SupplierProductBuilder SupplierProduct()
        {
            return new SupplierProductBuilder(new SupplierProduct(ProductVariant()));
        }
    }
}