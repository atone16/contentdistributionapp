using CDA.GraphQL.Queries;
using CDA.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types.QueryType
{
    public static class TenantQueryConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field("tenant")
                .Name("tenant")
                .Description("the tenant - the name of the group. where all relations will be attached to.")
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the tenant to find."))
                .Type<TenantType>()
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<string>("id");
                    if (Guid.TryParse(id, out Guid guidId))
                    {
                        var tenantManager = context.Service<ITenantManager>();
                        return await tenantManager.GetById(guidId);
                    }

                    throw new Exception("Invalid input parameters");
                });
        }
    }
}
