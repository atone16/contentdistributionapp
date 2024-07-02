using CDA.Data;
using CDA.GraphQL.Mutations;
using CDA.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types.MutationType
{
    public static class OrderMutationConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field("createOrder")
                .Description("Creates an order.")
                .Type<OrderType>()
                .Argument(
                    "orderInput",
                    a => a
                        .Type<NonNullType<BriefInputType>>()
                        .Description("The order to create."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<OrderInput>("orderInput");
                        var orderManager = context.Service<IOrderManager>();
                        return await orderManager.CreateOrder(input);
                    }
                );

            descriptor.Field("archiveOrder")
                .Description("Archives an order.")
                .Type<BooleanType>()
                .Argument(
                    "orderId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the order to archive."))
                .Resolve(
                    async context =>
                    {
                        var orderId = context.ArgumentValue<string>("orderId");
                        if (Guid.TryParse(orderId, out Guid guidId))
                        {
                            var orderManager = context.Service<IOrderManager>();
                            return await orderManager.ArchiveOrder(guidId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateOrder")
                .Description("Updates an order.")
                .Type<OrderType>()
                .Argument(
                    "orderId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the order to update."))
                .Argument(
                    "orderInput",
                    a => a
                        .Type<NonNullType<OrderInputType>>()
                        .Description("The order metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var orderId = context.ArgumentValue<string>("orderId");

                        if (Guid.TryParse(orderId, out Guid guidId))
                        {
                            var orderInput = context.ArgumentValue<OrderInput>("orderInput");
                            var orderManager = context.Service<IOrderManager>();
                            return await orderManager.UpdateOrder(guidId, orderInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );
        }
    }
}
