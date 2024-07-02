using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class AssetInputType : InputObjectType<AssetInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AssetInput> descriptor)
        {
            descriptor.Name("AssetInput");
            descriptor.Description("Input argument for inputting assets");

            descriptor.Field(x => x.AssetStatus);
            descriptor.Field(x => x.AssetId);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.FilePath);
            descriptor.Field(x => x.Format);
            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.Preview);
            descriptor.Field(x => x.Type);

            descriptor.Ignore(x => x.TenantId);
            descriptor.Ignore(x => x.UserId);
        }
    }
}
