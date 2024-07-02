using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class AssetCommentInputType : InputObjectType<AssetCommentInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AssetCommentInput> descriptor)
        {
            descriptor.Name("AssetCommentInput");
            descriptor.Description("Input argument for asset comment");

            descriptor.Field(x => x.AssetGuid);
            descriptor.Field(x => x.Comment);

            descriptor.Ignore(x => x.TenantId);
            descriptor.Ignore(x => x.UserId);
        }
    }
}
