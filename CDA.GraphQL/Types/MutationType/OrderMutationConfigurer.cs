using CDA.Data;
using CDA.GraphQL.Mutations;
using CDA.IManagers;
using System.Security.Claims;
using CDA.Mock;
using System;

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
                        .Type<NonNullType<OrderInputType>>()
                        .Description("The order to create."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<OrderInput>("orderInput");
                        var orderManager = context.Service<IOrderManager>();
                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                        input.TenantId = claimsPrincipal.GetTenantId();
                        input.UserId = claimsPrincipal.GetUserId();

                        return await orderManager.CreateOrder(input);
                    }
                );

            descriptor.Field("archiveOrder")
                .Description("Archives an order.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the order to archive."))
                .Resolve(
                    async context =>
                    {
                        var orderId = context.ArgumentValue<string>("id");
                        if (Guid.TryParse(orderId, out Guid guidId))
                        {
                            var orderManager = context.Service<IOrderManager>();
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();

                            return await orderManager.ArchiveOrder(guidId, tenantId, userId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateOrder")
                .Description("Updates an order.")
                .Type<OrderType>()
                .Argument(
                    "id",
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
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var orderInput = context.ArgumentValue<OrderInput>("orderInput");
                            var orderManager = context.Service<IOrderManager>();
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                            orderInput.TenantId = claimsPrincipal.GetTenantId();
                            orderInput.UserId = claimsPrincipal.GetUserId();

                            return await orderManager.UpdateOrder(guidId, orderInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("addBriefToOrder")
                .Description("Adds a brief to an order")
                .Type<OrderBriefType>()
                .Argument(
                    "orderBriefInput",
                    a => a
                        .Type<NonNullType<OrderBriefInputType>>()
                        .Description("The id of the order to update."))
                .Resolve(
                    async context =>
                    {
                        var orderBriefInput = context.ArgumentValue<OrderBriefInput>("orderBriefInput");
                        var orderManager = context.Service<IOrderManager>();
                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                        orderBriefInput.TenantId = claimsPrincipal.GetTenantId();
                        orderBriefInput.UserId = claimsPrincipal.GetUserId();

                        return await orderManager.AddBriefToOrder(orderBriefInput);
                    }
                );

            descriptor.Field("removeBriefToOrder")
                .Description("Removes a brief to an order")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the orderBrief to remove."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");
                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var orderManager = context.Service<IOrderManager>();
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            return await orderManager.RemoveBriefToOrder(guidId, claimsPrincipal.GetTenantId());
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );
        }
    }
}
