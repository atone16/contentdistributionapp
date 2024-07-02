using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{ 
    public class AssetVersionInputType : InputObjectType<AssetVersionInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<AssetVersionInput> descriptor)
        {
            descriptor.Name("AssetVersionInput");
            descriptor.Description("Input argument for asset versions");

            descriptor.Field(x => x.AssetGuid);
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
