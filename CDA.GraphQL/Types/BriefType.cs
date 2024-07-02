using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class BriefType : ObjectType<BriefDto>
    {
        protected override void Configure(IObjectTypeDescriptor<BriefDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.Name);
            descriptor.Field(x => x.Description);
            descriptor.Field(x => x.Status);
        }
    }
}
