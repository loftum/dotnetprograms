using System;
using System.Web.Mvc;
using AutoMapper;
using MasterData.Core.Domain;
using MasterData.Core.Domain.Pricing;
using MasterData.Core.Domain.Products;
using MasterData.Core.Domain.Stores;
using MasterData.Core.Model;
using MasterData.Core.Model.Misc;
using MasterData.Core.Model.Products;
using MasterData.Core.Model.Stores;
using MasterData.Core.Model.Suppliers;
using WebShop.Common.ExtensionMethods;
using WebShop.Common.Models.Pricing;

namespace MasterData.Core.Mapping
{
    public class MasterDataMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Price, PriceModel>();
            CreateMap<Salespoint, SalespointModel>();
            CreateMap<Salespoint, EditSalespointModel>()
                .ForMember(m => m.ResellerId, o => o.MapFrom(s => s.Reseller.Id))
                .ForMember(m => m.ResellerName, o => o.MapFrom(s => s.Reseller.Name));
            CreateMap<Reseller, EditResellerModel>()
                .Ignore(m => m.Suppliers);
            CreateMap<Reseller, ResellerModel>();
            CreateMap<Color, EditColorModel>();
            CreateMap<Color, ColorModel>()
                .Ignore(m => m.Rgb);
            CreateMap<ProductVariant, ProductVariantModel>();
            CreateMap<ProductMaster, ProductMasterModel>();
            CreateMap<ProductMaster, EditProductMasterModel>()
                .ForMember(m => m.ProducerId, o => o.MapFrom(p => p.Producer == null ? default(Guid) : p.Producer.Id))
                .ForMember(m => m.ProductTypeId, o => o.MapFrom(p => p.ProductType == null ? default(Guid) : p.ProductType.Id))
                .Ignore(m => m.AvailableProducers)
                .Ignore(m => m.AvailableProductTypes);
            CreateMap<ProductVariant, EditProductVariantModel>()
                .ForMember(m => m.MasterId, o => o.MapFrom(v => v.Master.Id))
                .ForMember(m => m.Description, o => o.MapFrom(d => new InheritableModel<string>(d.GetDescription())))
                .Ignore(m => m.AvailableColors);
            CreateMap<ProductType, ProductTypeModel>();
            CreateMap<Producer, ProducerModel>();
            CreateMap<Supplier, SupplierModel>();
            CreateMap<Supplier, EditSupplierModel>()
                .Ignore(m => m.Products);
            CreateMap<IHaveName, SelectListItem>()
                .ForMember(i => i.Text, o => o.MapFrom(n => n.Name))
                .ForMember(i => i.Value, o => o.MapFrom(n => n.Id.ToString()))
                .Ignore(i => i.Selected);
        }
    }
}