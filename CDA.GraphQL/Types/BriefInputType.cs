using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class BriefInputType : InputObjectType<BriefInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<BriefInput> descriptor)
        {
            descriptor.Name("BriefInput");
            descriptor.Description("Input argument for brief to create.");

            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.Status);
            descriptor.Field(x => x.AssetId);
        }
    }
}
