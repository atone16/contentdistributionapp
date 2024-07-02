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
                        var input = context.ArgumentValue<BriefInput>("assetInput");
                        var briefManager = context.Service<IBriefManager>();
                        return await briefManager.CreateBrief(input);
                    }
                );

            descriptor.Field("archiveBrief")
                .Description("Archives an asset and its related data.")
                .Type<BooleanType>()
                .Argument(
                    "briefId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The brief to archive."))
                .Resolve(
                    async context =>
                    {
                        var briefId = context.ArgumentValue<string>("briefId");
                        if (Guid.TryParse(briefId, out Guid guidId))
                        {
                            var briefManager = context.Service<IBriefManager>();
                            return await briefManager.ArchiveBrief(guidId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateBrief")
                .Description("Updates a brief.")
                .Type<BriefType>()
                .Argument(
                    "briefId",
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
                        var briefId = context.ArgumentValue<string>("briefId");

                        if (Guid.TryParse(briefId, out Guid guidId))
                        {
                            var briefInput = context.ArgumentValue<BriefInput>("briefInput");
                            var briefManager = context.Service<IBriefManager>();
                            return await briefManager.UpdateBrief(guidId, briefInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );
        }
    }
}
