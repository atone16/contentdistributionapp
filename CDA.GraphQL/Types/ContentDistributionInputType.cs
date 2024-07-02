using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class ContentDistributionInputType : InputObjectType<ContentDistributionInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<ContentDistributionInput> descriptor)
        {
            descriptor.Name("ContentDistributionInput");
            descriptor.Description("Input argument content distribution.");

            descriptor.Field(x => x.DistributionChannels);
            descriptor.Field(x => x.DistributionMethods);
            descriptor.Field(x => x.DistributionDate);
        }
    }
}
