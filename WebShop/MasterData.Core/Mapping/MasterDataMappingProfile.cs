using AutoMapper;
using MasterData.Core.Domain.MasterData;
using MasterData.Core.Domain.Pricing;
using MasterData.Core.Model;
using WebShop.Common.Models.Pricing;

namespace MasterData.Core.Mapping
{
    public class MasterDataMappingProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<Price, PriceModel>();
            CreateMap<ProductMaster, ProductMasterModel>();
        }
    }
}