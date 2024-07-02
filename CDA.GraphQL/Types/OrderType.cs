using CDA.Data;
using CDA.IManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class OrderType : ObjectType<OrderDto>
    {
        internal const string OrderBriefsDescription = "The different briefs tied to an order.";

        protected override void Configure(IObjectTypeDescriptor<OrderDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.CampaignName);
            descriptor.Field(x => x.OrderNumber);
            descriptor.Field(x => x.OrderDate);

            descriptor.Field("orderBriefs")
                .Type<ListType<OrderBriefType>>()
                .Description(OrderBriefsDescription)
                .Resolve(async context =>
                {
                    var orderManager = context.Service<IOrderManager>();
                    return await orderManager.GetOrderBriefsByOrderId(context.Parent<OrderDto>().Id, context.Parent<OrderDto>().TenantId);
                });

            descriptor.Field("totalBriefs")
                .Type<IntType>()
                .Description(OrderBriefsDescription)
                .Resolve(async context =>
                {
                    var orderManager = context.Service<IOrderManager>();
                    var result = await orderManager.GetOrderBriefsByOrderId(context.Parent<OrderDto>().Id, context.Parent<OrderDto>().TenantId);
                    return result.Count;
                });
        }
    }
}
