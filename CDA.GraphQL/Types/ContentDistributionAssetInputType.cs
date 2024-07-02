using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class ContentDistributionAssetInputType : InputObjectType<ContentDistributionAssetInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<ContentDistributionAssetInput> descriptor)
        {
            descriptor.Name("ContentDistributionAssetInput");
            descriptor.Description("Input argument content distribution asset.");

            descriptor.Field(x => x.ContentDistributionId);
            descriptor.Field(x => x.AssetId);
            descriptor.Field(x => x.FileUrl);
            descriptor.Field(x => x.Name);

            descriptor.Ignore(x => x.UserId);
            descriptor.Ignore(x => x.TenantId);
        }
    }
}
