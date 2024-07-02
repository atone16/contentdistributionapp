using AutoMapper;
using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Utilities
{
    public class CDAMapperProfile : Profile
    {
        public CDAMapperProfile() 
        {
            CreateMap<Tenant, TenantInput>();
            CreateMap<TenantInput, Tenant>();
            CreateMap<Tenant, TenantDto>();

            CreateMap<User, UserInput>();
            CreateMap<UserInput, User>();
            CreateMap<User, UserDto>();

            CreateMap<Asset, AssetInput>();
            CreateMap<AssetInput, Asset>();
            CreateMap<Asset, AssetDto>();

            CreateMap<AssetComment, AssetCommentInput>();
            CreateMap<AssetCommentInput, AssetComment>();
            CreateMap<AssetComment, AssetCommentDto>();


            CreateMap<Order, OrderInput>();
            CreateMap<OrderInput, Order>();
            CreateMap<Order, OrderDto>();

            CreateMap<OrderBrief, OrderBriefInput>();
            CreateMap<OrderBriefInput, OrderBrief>();
            CreateMap<OrderBrief, OrderBriefDto>();

            CreateMap<Brief, BriefInput>();
            CreateMap<BriefInput, Brief>();
            CreateMap<Brief, BriefDto>();

            CreateMap<BriefComment, BriefCommentInput>();
            CreateMap<BriefCommentInput, BriefComment>();
            CreateMap<BriefComment, BriefCommentDto>();

            CreateMap<ContentDistribution, ContentDistributionInput>();
            CreateMap<ContentDistributionInput, ContentDistribution>();
            CreateMap<ContentDistribution, ContentDistributionDto>();

            CreateMap<ContentDistributionAsset, ContentDistributionAssetInput>();
            CreateMap<ContentDistributionAssetInput, ContentDistributionAsset>();
            CreateMap<ContentDistributionAsset, ContentDistributionAssetDto>();

        }
    }
}
