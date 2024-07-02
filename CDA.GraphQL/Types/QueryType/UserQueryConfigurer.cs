using CDA.GraphQL.Queries;
using CDA.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types.QueryType
{
    public static class UserQueryConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field("user")
                .Name("user")
                .Description("the user")
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the user to find."))
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
