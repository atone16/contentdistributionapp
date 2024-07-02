using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class TenantType : ObjectType<TenantDto>
    {
        protected override void Configure(IObjectTypeDescriptor<TenantDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantName);
        }
    }
}
