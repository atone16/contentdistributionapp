using CDA.GraphQL.Queries;
using CDA.IManagers;

namespace CDA.GraphQL.Types.QueryType
{
    public static class OrderQueryConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field("order")
                .Name("order")
                .Description("the order")
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the order to find."))
                .Type<OrderType>()
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<string>("id");
                    if (Guid.TryParse(id, out Guid guidId))
                    {
                        var orderManager = context.Service<IOrderManager>();
                        return await orderManager.GetById(guidId);
                    }
                    throw new Exception("Invalid input parameters");
                });
        }
    }
}
