using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class AssetUpdateInputType : InputObjectType<AssetUpdateInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AssetUpdateInput> descriptor)
        {
            descriptor.Name("AssetUpdateInput");
            descriptor.Description("Input argument for inputting assets");

            descriptor.Field(x => x.AssetStatus);
            descriptor.Field(x => x.AssetId);
            descriptor.Field(x => x.AssignedUserId);

            descriptor.Ignore(x => x.TenantId);
            descriptor.Ignore(x => x.UserId);
        }
    }
}
