using CDA.Data;
using CDA.IManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class ContentDistributionType : ObjectType<ContentDistributionDto>
    {
        internal const string Assets = "The list of assets tied to content distribution.";
        protected override void Configure(IObjectTypeDescriptor<ContentDistributionDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.DistributionChannels);
            descriptor.Field(x => x.DistributionMethods);
            descriptor.Field(x => x.DistributionDate);

            descriptor.Field("contentDistributionAssets")
                .Type<ListType<ContentDistributionAssetType>>()
                .Description(Assets)
                .Resolve(async context =>
                {
                    var contentDistributionManager = context.Service<IContentDistributionManager>();
                    return await contentDistributionManager.GetByContentDistribution(context.Parent<ContentDistributionDto>().Id, context.Parent<ContentDistributionDto>().TenantId);
                });
        }
    }
}
