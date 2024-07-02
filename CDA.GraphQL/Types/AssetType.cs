using CDA.Data;
using CDAEnum = CDA.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CDA.IManagers;

namespace CDA.GraphQL.Types
{
    public class AssetType : ObjectType<AssetDto>
    {
        internal const string AssetComments = "The list of comments with their respective user for assets.";

        protected override void Configure(IObjectTypeDescriptor<AssetDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.AssetId);
            descriptor.Field(x => x.AssignedUserId);
            descriptor.Field(x => x.AssetStatus).Type<EnumType<CDAEnum.Status>>();
            descriptor.Field(x => x.AssetVersions);

            descriptor.Field("comments")
                .Type<ListType<AssetCommentType>>()
                .Description(AssetComments)
                .Resolve(async context =>
                {
                    var assetManager = context.Service<IAssetManager>();
                    return await assetManager.GetByAssetId(context.Parent<AssetDto>().Id, context.Parent<AssetDto>().TenantId);
                });
        }
    }
}
