using CDA.Data;
using CDA.IManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class BriefType : ObjectType<BriefDto>
    {
        internal const string BriefComments = "The list of comments with their respective user for briefs.";
        internal const string AssetDesc = "The Asset tied to a Brief";

        protected override void Configure(IObjectTypeDescriptor<BriefDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.Status);

            descriptor.Field("comments")
                .Type<ListType<BriefCommentType>>()
                .Description(BriefComments)
                .Resolve(async context =>
                {
                    var briefManager = context.Service<IBriefManager>();
                    return await briefManager.GetByBriefId(context.Parent<BriefDto>().Id, context.Parent<BriefDto>().TenantId);
                });

            descriptor.Field("asset")
                .Type<AssetType>()
                .Description(AssetDesc)
                .Resolve(async context =>
                {
                    var assetManager = context.Service<IAssetManager>();
                    return await assetManager.GetByAssetId(context.Parent<BriefDto>().AssetId);
                });
        }
    }
}
