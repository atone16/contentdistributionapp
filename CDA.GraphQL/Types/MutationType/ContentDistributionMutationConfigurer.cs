using CDA.Data;
using CDA.GraphQL.Mutations;
using CDA.IManagers;
using System.Security.Claims;
using CDA.Mock;

namespace CDA.GraphQL.Types.MutationType
{
    public static class ContentDistributionMutationConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field("createContentDistribution")
                .Description("Creates Content Distribution.")
                .Type<ContentDistributionType>()
                .Argument(
                    "contentDistributionInput",
                    a => a
                        .Type<NonNullType<ContentDistributionInputType>>()
                        .Description("The content distribution to create."))
                .Resolve(
                    async context =>
                    {
                        var contentDistributionInput = context.ArgumentValue<ContentDistributionInput>("contentDistributionInput");
                        var contentDistributionManager = context.Service<IContentDistributionManager>();
                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                        contentDistributionInput.TenantId = claimsPrincipal.GetTenantId();
                        contentDistributionInput.UserId = claimsPrincipal.GetUserId();
                        return await contentDistributionManager.CreateContentDistribution(contentDistributionInput);
                    }
                );

            descriptor.Field("archiveContentDistribution")
                .Description("Archives Content Distribution.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the content distribution to archive."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");
                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var contentDistributionManager = context.Service<IContentDistributionManager>();
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();

                            return await contentDistributionManager.ArchiveContentDistribution(guidId, tenantId, userId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateContentDistribution")
                .Description("Updates Content Distribution.")
                .Type<ContentDistributionType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the content distribution to update."))
                .Argument(
                    "contentDistributionInput",
                    a => a
                        .Type<NonNullType<ContentDistributionInputType>>()
                        .Description("The content distribution metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var contentDistributionInput = context.ArgumentValue<ContentDistributionInput>("contentDistributionInput");
                            var contentDistributionManager = context.Service<IContentDistributionManager>();
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                            contentDistributionInput.TenantId = claimsPrincipal.GetTenantId();
                            contentDistributionInput.UserId = claimsPrincipal.GetUserId();
                            return await contentDistributionManager.UpdateContentDistribution(guidId, contentDistributionInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("addContentDistributionAsset")
                .Description("Add Content Distribution Asset.")
                .Type<ContentDistributionAssetType>()
                .Argument(
                    "contentDistributionAssetInput",
                    a => a
                        .Type<NonNullType<ContentDistributionAssetInputType>>()
                        .Description("The content distribution asset metadata."))
                .Resolve(
                    async context =>
                    {
                        var contentDistributionAssetInput = context.ArgumentValue<ContentDistributionAssetInput>("contentDistributionAssetInput");
                        var contentDistributionManager = context.Service<IContentDistributionManager>();
                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                        contentDistributionAssetInput.TenantId = claimsPrincipal.GetTenantId();
                        contentDistributionAssetInput.UserId = claimsPrincipal.GetUserId();

                        return await contentDistributionManager.AddContentDistributionAsset(contentDistributionAssetInput);
                    }
                );


            descriptor.Field("updateContentDistributionAsset")
                .Description("Update Content Distribution Asset.")
                .Type<ContentDistributionAssetType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the order to update."))
                .Argument(
                    "contentDistributionAssetInput",
                    a => a
                        .Type<NonNullType<ContentDistributionAssetInputType>>()
                        .Description("The content distribution asset metadata."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var contentDistributionInput = context.ArgumentValue<ContentDistributionAssetInput>("contentDistributionAssetInput");
                            var contentDistributionManager = context.Service<IContentDistributionManager>();
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                            contentDistributionInput.TenantId = claimsPrincipal.GetTenantId();
                            contentDistributionInput.UserId = claimsPrincipal.GetUserId();
                            return await contentDistributionManager.UpdateContentDistributionAsset(guidId, contentDistributionInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );


            descriptor.Field("deleteContentDistributionAsset")
                .Description("Delete Content Distribution Asset.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the order to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var contentDistributionManager = context.Service<IContentDistributionManager>();
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();
                            return await contentDistributionManager.RemoveContentDistributionAsset(guidId, tenantId, userId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );
        }

    }
}
