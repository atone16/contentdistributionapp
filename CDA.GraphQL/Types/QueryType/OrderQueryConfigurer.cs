using CDA.GraphQL.Queries;
using CDA.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Type<AssetType>()
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
