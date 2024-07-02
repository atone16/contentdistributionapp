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
                        return await contentDistributionManager.CreateContentDistribution(contentDistributionInput);
                    }
                );

            descriptor.Field("archiveContentDistribution")
                .Description("Archives Content Distribution.")
                .Type<BooleanType>()
                .Argument(
                    "contentDistributionId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the order to archive."))
                .Resolve(
                    async context =>
                    {
                        var contentDistributionId = context.ArgumentValue<string>("contentDistributionId");
                        if (Guid.TryParse(contentDistributionId, out Guid guidId))
                        {
                            var contentDistributionManager = context.Service<IContentDistributionManager>();
                            return await contentDistributionManager.ArchiveContentDistribution(guidId);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("updateContentDistribution")
                .Description("Updates Content Distribution.")
                .Type<ContentDistributionType>()
                .Argument(
                    "contentDistributionId",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the order to update."))
                .Argument(
                    "contentDistributionInput",
                    a => a
                        .Type<NonNullType<ContentDistributionInputType>>()
                        .Description("The order metadata to update."))
                .Resolve(
                    async context =>
                    {
                        var contentDistributionId = context.ArgumentValue<string>("contentDistributionId");

                        if (Guid.TryParse(contentDistributionId, out Guid guidId))
                        {
                            var contentDistributionInput = context.ArgumentValue<ContentDistributionInput>("contentDistributionInput");
                            var contentDistributionManager = context.Service<IContentDistributionManager>();
                            return await contentDistributionManager.UpdateContentDistribution(guidId, contentDistributionInput);
                        }
                        throw new Exception("Invalid input parameters");
                    }
                );
        }

    }
}
