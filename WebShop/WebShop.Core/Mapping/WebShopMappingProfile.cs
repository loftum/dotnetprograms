using AutoMapper;
using MasterData.Core.Domain.MasterData;
using MasterData.Core.Domain.Pricing;
using WebShop.Common.Models.Pricing;
using WebShop.Core.Model;

namespace WebShop.Core.Mapping
{
    public class WebShopMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Price, PriceModel>();
            CreateMap<SaleProduct, WebShopProductModel>()
                .ForMember(m => m.SaleProductId, o => o.MapFrom(sp => sp.Id))
                .ForMember(m => m.Name, o => o.MapFrom(sp => sp.GetName().Value))
                .ForMember(m => m.Description, o => o.MapFrom(sp => sp.GetDescription().Value))
                .ForMember(m => m.Price, o => o.MapFrom(sp => sp.CalculatedPrice));
        }
    }
}