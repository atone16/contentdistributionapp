using CDA.Data;
using CDA.GraphQL.Mutations;
using CDA.IManagers;
using System.Security.Claims;
using CDA.Mock;

namespace CDA.GraphQL.Types.MutationType
{
    public static class BriefMutationConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field("createBrief")
                .Description("Creates a brief.")
                .Type<BriefType>()
                .Argument(
                    "briefInput",
                    a => a
                        .Type<NonNullType<BriefInputType>>()
                        .Description("The brief to create."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<BriefInput>("briefInput");
                        var briefManager = context.Service<IBriefManager>();
                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));

                        input.TenantId = claimsPrincipal.GetTenantId();
                        input.UserId = claimsPrincipal.GetUserId();
                        return await briefManager.CreateBrief(input);
                    }
                );

            descriptor.Field("archiveBrief")
                .Description("Archives an asset and its related data.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The brief to archive."))
                .Resolve(
                    async context =>
                    {
                        var briefId = context.ArgumentValue<string>("id");
                        if (Guid.TryParse(briefId, out Guid guidId))
                        {
                            var briefManager = context.Service<IBriefManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();

                            return await briefManager.ArchiveBrief(guidId, tenantId, userId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateBrief")
                .Description("Updates a brief.")
                .Type<BriefType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The brief to update."))
                .Argument(
                    "briefInput",
                    a => a
                        .Type<NonNullType<BriefInputType>>()
                        .Description("The brief metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var briefId = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(briefId, out Guid guidId))
                        {
                            var input = context.ArgumentValue<BriefInput>("briefInput");
                            var briefManager = context.Service<IBriefManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            input.TenantId = claimsPrincipal.GetTenantId();
                            input.UserId = claimsPrincipal.GetUserId();

                            return await briefManager.UpdateBrief(guidId, input);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("addBriefComment")
                .Description("add a comment for a brief.")
                .Type<BriefCommentType>()
                .Argument(
                    "briefCommentInput",
                    a => a
                        .Type<NonNullType<BriefCommentInputType>>()
                        .Description("The brief comment metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<BriefCommentInput>("briefCommentInput");
                        var briefManager = context.Service<IBriefManager>();

                        var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                        input.TenantId = claimsPrincipal.GetTenantId();
                        input.UserId = claimsPrincipal.GetUserId();

                        return await briefManager.AddBriefComment(input);
                    }
                );

            descriptor.Field("updateBriefComment")
                .Description("Updates a comment for brief.")
                .Type<BriefCommentType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The brief to update."))
                .Argument(
                    "briefCommentInput",
                    a => a
                        .Type<NonNullType<BriefCommentInputType>>()
                        .Description("The brief metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var input = context.ArgumentValue<BriefCommentInput>("briefCommentInput");
                            var briefManager = context.Service<IBriefManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            input.TenantId = claimsPrincipal.GetTenantId();
                            input.UserId = claimsPrincipal.GetUserId();

                            return await briefManager.UpdateBriefComment(guidId, input);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("removeBriefComment")
                .Description("Removes a comment for a brief.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The brief to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var briefManager = context.Service<IBriefManager>();

                            var claimsPrincipal = context.GetGlobalStateOrDefault<ClaimsPrincipal>(nameof(ClaimsPrincipal));
                            var tenantId = claimsPrincipal.GetTenantId();
                            var userId = claimsPrincipal.GetUserId();

                            return await briefManager.RemoveBriefComment(guidId, tenantId, userId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );
        }
    }
}
