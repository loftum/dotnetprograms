using AutoMapper;
using WebShop.Core.Domain.MasterData;
using WebShop.Core.Model;

namespace WebShop.Core.Mapping
{
    public class MasterDataMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Price, PriceModel>();
            CreateMap<Product, ProductModel>();
        }
    }
}