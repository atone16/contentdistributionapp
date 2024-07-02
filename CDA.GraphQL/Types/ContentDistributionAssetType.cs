using CDA.Data;
using CDA.IManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class ContentDistributionAssetType : ObjectType<ContentDistributionAssetDto>
    {
        internal const string AssetDesc = "The Asset tied to ContentDistribution";

        protected override void Configure(IObjectTypeDescriptor<ContentDistributionAssetDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.AssetId);
            descriptor.Field(x => x.FileUrl);
            descriptor.Field(x => x.ContentDistributionId);

            descriptor.Field("asset")
                .Type<AssetType>()
                .Description(AssetDesc)
                .Resolve(async context =>
                {
                    var assetManager = context.Service<IAssetManager>();
                    return await assetManager.GetByAssetId(context.Parent<ContentDistributionAssetDto>().AssetId);
                });
        }
    }
}
