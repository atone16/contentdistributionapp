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
        }
    }
}
