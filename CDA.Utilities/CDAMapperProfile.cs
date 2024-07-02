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
            CreateMap<Asset, AssetInput>();
            CreateMap<AssetInput, Asset>();
            CreateMap<Asset, AssetDto>();

            CreateMap<Tenant, TenantInput>();
            CreateMap<TenantInput, Tenant>();
            CreateMap<Tenant, TenantDto>();

            CreateMap<User, UserInput>();
            CreateMap<UserInput, User>();
            CreateMap<User, UserDto>();
        }
    }
}
