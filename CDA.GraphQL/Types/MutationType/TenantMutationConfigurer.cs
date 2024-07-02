using CDA.Data;
using CDA.GraphQL.Mutations;
using CDA.IManagers;

namespace CDA.GraphQL.Types.MutationType
{
    public static class TenantMutationConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field("createTenant")
                .Description("Creates a new tenant. This is where all content will be associated with.")
                .Type<TenantType>()
                .Argument(
                    "tenantInput",
                    a => a
                        .Type<NonNullType<TenantInputType>>()
                        .Description("The tenant to create."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<TenantInput>("tenantInput");
                        var tenantManager = context.Service<ITenantManager>();
                        return await tenantManager.CreateTenant(input);
                    }
                );

            descriptor.Field("updateTenant")
                .Description("Updates an existing tenant.")
                .Type<TenantType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the tenant."))
                .Argument(
                    "tenantInput",
                    a => a
                        .Type<NonNullType<TenantInputType>>()
                        .Description("The tenant information to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var input = context.ArgumentValue<TenantInput>("tenantInput");
                            var tenantManager = context.Service<ITenantManager>();
                            return await tenantManager.UpdateTenant(guidId, input);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );
        }
    }
}
