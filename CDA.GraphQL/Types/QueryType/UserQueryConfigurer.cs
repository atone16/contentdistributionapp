using CDA.GraphQL.Queries;
using CDA.IManagers;
using CDA.Mock;
using System.Security.Claims;

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
                .Type<UserType>()
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<string>("id");
                    if (Guid.TryParse(id, out Guid guidId))
                    {
                        var userManager = context.Service<IUserManager>();
                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                        var tenantId = claimsPrincipal.GetTenantId();
                        return await userManager.GetById(guidId, tenantId);
                    }
                    throw new Exception("Invalid input parameters");
                });
        }
    }
}
