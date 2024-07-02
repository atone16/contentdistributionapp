using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class OrderType : ObjectType<OrderDto>
    {
        protected override void Configure(IObjectTypeDescriptor<OrderDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.CampaignName);
            descriptor.Field(x => x.OrderNumber);
            descriptor.Field(x => x.OrderDate);
        }
    }
}
