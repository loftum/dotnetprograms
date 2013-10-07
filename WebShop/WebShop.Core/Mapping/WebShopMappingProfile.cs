using AutoMapper;
using MasterData.Core.Domain.MasterData;
using MasterData.Core.Domain.Pricing;
using WebShop.Common.ExtensionMethods;
using WebShop.Common.Models.Pricing;
using WebShop.Core.Domain.OrderDb;
using WebShop.Core.ExtensionMethods;
using WebShop.Core.Model;
using WebShop.Core.Users;

namespace WebShop.Core.Mapping
{
    public class WebShopMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Price, PriceModel>()
                .ConvertUsing(p => new PriceModel(p.IncVat, p.ExVat));
            CreateMap<SaleProduct, WebShopProductModel>()
                .ForMember(m => m.SaleProductId, o => o.MapFrom(sp => sp.Id))
                .ForMember(m => m.Name, o => o.MapFrom(sp => sp.GetName().Value))
                .ForMember(m => m.Number, o => o.MapFrom(sp => sp.ProductNumber))
                .ForMember(m => m.Description, o => o.MapFrom(sp => sp.GetDescription().Value))
                .ForMember(m => m.Price, o => o.MapFrom(sp => sp.CalculatedPrice));

            CreateMap<PriceModel, Price>()
                .ConvertUsing(m => new Price(m.IncVat, m.ExVat));
            CreateMap<PersonaliaModel, Buyer>();
            CreateMap<BasketModel, OrderHead>()
                .ForMember(o => o.Buyer, o => o.MapFrom(b => b.Personalia))
                .ForMember(o => o.Lines, opt => opt.MapFrom(b => b.Items))
                .IgnoreOrderDbObjectProperties()
                .Ignore(o => o.OrderNumber);

            CreateMap<BasketItemModel, OrderLine>()
                .ForMember(l => l.ProductNumber, o => o.MapFrom(i => i.Product.Number))
                .ForMember(l => l.ProductName, o => o.MapFrom(i => i.Product.Name))
                .ForMember(l => l.Price, o => o.MapFrom(i => i.Price))
                .IgnoreOrderDbObjectProperties()
                .Ignore(l => l.OrderHead);
            CreateMap<OrderHead, ReceiptModel>();
        }
    }
}