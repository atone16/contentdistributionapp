using CDA.Data;
using CDA.IManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class TenantType : ObjectType<TenantDto>
    {
        internal const string UsersDescription = "The List of users tied to a tenant.";
        internal const string OrdersDescription = "The List of orders tied to a tenant.";
        internal const string BriefsDescription = "The List of briefs tied to a tenant.";
        internal const string ContentDistributionsDescription = "The List of contentDistributions tied to a tenant.";
        internal const string AssetsDescription = "The List of assets tied to a tenant.";

        protected override void Configure(IObjectTypeDescriptor<TenantDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantName);

            descriptor.Field("users")
                .Type<ListType<UserType>>()
                .Description(UsersDescription)
                .Resolve(async context =>
                {
                    var userManager = context.Service<IUserManager>();
                    return await userManager.GetByTenantId(context.Parent<TenantDto>().Id);
                });

            descriptor.Field("orders")
                .Type<ListType<OrderType>>()
                .Description(OrdersDescription)
                .Resolve(async context =>
                {
                    var orderManager = context.Service<IOrderManager>();
                    return await orderManager.GetByTenantId(context.Parent<TenantDto>().Id);
                });

            descriptor.Field("briefs")
                .Type<ListType<BriefType>>()
                .Description(BriefsDescription)
                .Resolve(async context =>
                {
                    var briefManager = context.Service<IBriefManager>();
                    return await briefManager.GetByTenantId(context.Parent<TenantDto>().Id);
                });

            descriptor.Field("contentDistributions")
                .Type<ListType<ContentDistributionType>>()
                .Description(ContentDistributionsDescription)
                .Resolve(async context =>
                {
                    var contentDistributionManager = context.Service<IContentDistributionManager>();
                    return await contentDistributionManager.GetByTenantId(context.Parent<TenantDto>().Id);
                });

            descriptor.Field("assets")
                .Type<ListType<AssetType>>()
                .Description(AssetsDescription)
                .Resolve(async context =>
                {
                    var assetManager = context.Service<IAssetManager>();
                    return await assetManager.GetByTenantId(context.Parent<TenantDto>().Id);
                });
        }
    }
}
