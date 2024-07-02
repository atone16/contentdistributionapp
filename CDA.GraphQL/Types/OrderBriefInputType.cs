using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class OrderBriefInputType : InputObjectType<OrderBriefInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<OrderBriefInput> descriptor)
        {
            descriptor.Name("OrderBriefInput");
            descriptor.Description("Input argument to add brief to input.");

            descriptor.Field(x => x.BriefId);
            descriptor.Field(x => x.Quantity);

            descriptor.Ignore(x => x.UserId);
            descriptor.Ignore(x => x.TenantId);
        }
    }
}
