using CDA.Data;
using CDA.IManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class OrderBriefType : ObjectType<OrderBriefDto>
    {
        internal const string BriefDesc = "The brief tied to an order brief";

        protected override void Configure(IObjectTypeDescriptor<OrderBriefDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.Quantity);
            descriptor.Field(x => x.BriefId);

            descriptor.Field("brief")
                .Type<BriefType>()
                .Description(BriefDesc)
                .Resolve(async context =>
                {
                    var briefManager = context.Service<IBriefManager>();
                    return await briefManager.GetByBriefId(context.Parent<OrderBriefDto>().BriefId);
                });
        }
    }
}
