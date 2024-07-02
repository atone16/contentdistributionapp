using CDA.Data;
using CDA.GraphQL.Mutations;
using CDA.IManagers;
using CDA.Mock;
using System.Security.Claims;

namespace CDA.GraphQL.Types.MutationType
{
    public static class AssetMutationConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field("createAsset")
                .Description("Creates a new asset tied to the tenant.")
                .Type<AssetType>()
                .Argument(
                    "assetInput",
                    a => a
                        .Type<NonNullType<AssetInputType>>()
                        .Description("The asset to create."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<AssetInput>("assetInput");
                        var assetManager = context.Service<IAssetManager>();

                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                        input.TenantId = claimsPrincipal.GetTenantId();
                        input.UserId = claimsPrincipal.GetUserId();

                        return await assetManager.CreateAsset(input);
                    }
                );

            descriptor.Field("archiveAsset")
                .Description("Archives an asset and its related data.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The asset to archive."))
                .Resolve(
                    async context =>
                    {
                        var assetId = context.ArgumentValue<string>("id");
                        if (Guid.TryParse(assetId, out Guid guidId))
                        {
                            var assetManager = context.Service<IAssetManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();

                            return await assetManager.ArchiveAsset(guidId, tenantId, userId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateAsset")
                .Description("Updates an asset.")
                .Type<AssetType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The asset to update."))
                .Argument(
                    "assetUpdateInput",
                    a => a
                        .Type<NonNullType<AssetUpdateInputType>>()
                        .Description("The asset metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");
                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var assetUpdateInput = context.ArgumentValue<AssetUpdateInput>("assetUpdateInput");
                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                            assetUpdateInput.TenantId = claimsPrincipal.GetTenantId();
                            assetUpdateInput.UserId = claimsPrincipal.GetUserId();

                            var assetManager = context.Service<IAssetManager>();
                            return await assetManager.UpdateAsset(guidId, assetUpdateInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("addAssetComment")
                .Description("add a comment for an asset.")
                .Type<AssetCommentType>()
                .Argument(
                    "assetCommentInput",
                    a => a
                        .Type<NonNullType<AssetCommentInputType>>()
                        .Description("The asset comment metadata to add."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<AssetCommentInput>("assetCommentInput");
                        var assetManager = context.Service<IAssetManager>();

                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                        input.TenantId = claimsPrincipal.GetTenantId();
                        input.UserId = claimsPrincipal.GetUserId();

                        return await assetManager.AddAssetComment(input);
                    }
                );

            descriptor.Field("updateAssetComment")
                .Description("Updates a comment for an asset.")
                .Type<AssetCommentType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The asset comment to update."))
                .Argument(
                    "assetCommentInput",
                    a => a
                        .Type<NonNullType<AssetCommentInputType>>()
                        .Description("The assetcoment metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var input = context.ArgumentValue<AssetCommentInput>("assetCommentInput");
                            var assetManager = context.Service<IAssetManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            input.TenantId = claimsPrincipal.GetTenantId();
                            input.UserId = claimsPrincipal.GetUserId();

                            return await assetManager.UpdateAssetComment(guidId, input);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("removeAssetComment")
                .Description("Removes a comment for an asset.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The asset comment to remove."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var assetManager = context.Service<IAssetManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();

                            return await assetManager.RemoveAssetComment(guidId, tenantId, userId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("removeAssetVersion")
                .Description("Removes an asset version for an asset.")
                .Type<AssetType>()
                .Argument(
                    "assetGuid",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The assetId to update asset version."))

                .Argument(
                    "assetVersionNumber",
                    a => a
                        .Type<NonNullType<IntType>>()
                        .Description("The asset comment to remove."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("assetGuid");
                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var assetVersionNumber = context.ArgumentValue<int>("assetVersionNumber");
                            var assetManager = context.Service<IAssetManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();

                            return await assetManager.RemoveAssetVersion(guidId, userId, assetVersionNumber);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("addAssetVersion")
                .Description("Add an asset version for an asset.")
                .Type<AssetType>()
                .Argument(
                    "assetVersionInput",
                    a => a
                        .Type<NonNullType<AssetVersionInputType>>()
                        .Description("InputMetadata for Asset Version to Add."))
                .Resolve(
                    async context =>
                    {
                        var assetVersionInput = context.ArgumentValue<AssetVersionInput>("assetVersionInput");
                        var assetManager = context.Service<IAssetManager>();

                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                        assetVersionInput.TenantId = claimsPrincipal.GetTenantId();
                        assetVersionInput.UserId = claimsPrincipal.GetUserId();

                        return await assetManager.AddNewAssetVersion(assetVersionInput);
                    }
                );
        }
    }
}
