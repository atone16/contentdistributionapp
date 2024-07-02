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
                        return await assetManager.CreateAsset(input);
                    }
                );

            descriptor.Field("archiveAsset")
                .Description("Archives an asset and its related data.")
                .Type<BooleanType>()
                .Argument(
                    "assetId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The asset to archive."))
                .Resolve(
                    async context =>
                    {
                        var assetId = context.ArgumentValue<string>("assetId");
                        if (Guid.TryParse(assetId, out Guid guidId))
                        {
                            var assetManager = context.Service<IAssetManager>();
                            return await assetManager.ArchiveAsset(guidId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("unArchiveAsset")
                .Description("Unarchives an asset and its related data.")
                .Type<BooleanType>()
                .Argument(
                    "assetId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The asset to unarchive."))
                .Resolve(
                    async context =>
                    {
                        var assetId = context.ArgumentValue<string>("assetId");
                        if (Guid.TryParse(assetId, out Guid guidId))
                        {
                            var assetManager = context.Service<IAssetManager>();
                            return await assetManager.UnArchiveAsset(guidId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateAsset")
                .Description("Updates an asset.")
                .Type<AssetType>()
                .Argument(
                    "assetId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The asset to update."))
                .Argument(
                    "assetInput",
                    a => a
                        .Type<NonNullType<AssetInputType>>()
                        .Description("The asset metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var assetId = context.ArgumentValue<string>("assetId");
                        if (Guid.TryParse(assetId, out Guid guidId))
                        {
                            var assetInput = context.ArgumentValue<AssetInput>("assetInput");

                            var assetManager = context.Service<IAssetManager>();
                            return await assetManager.UpdateAsset(Guid.Parse(assetId), assetInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

        }
    }
}
