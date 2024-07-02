using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class OrderInputType : InputObjectType<OrderInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<OrderInput> descriptor)
        {
            descriptor.Name("OrderInput");
            descriptor.Description("Input argument for order to create.");

            descriptor.Field(x => x.OrderDate);
            descriptor.Field(x => x.OrderNumber);
            descriptor.Field(x => x.CampaignName);
            descriptor.Field(x => x.RequesterUserId);

            descriptor.Ignore(x => x.UserId);
            descriptor.Ignore(x => x.TenantId);
        }
    }
}
