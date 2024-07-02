using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class ContentDistributionType : ObjectType<ContentDistributionDto>
    {
        protected override void Configure(IObjectTypeDescriptor<ContentDistributionDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.DistributionChannels);
            descriptor.Field(x => x.DistributionMethods);
            descriptor.Field(x => x.DistributionDate);
        }
    }
}
