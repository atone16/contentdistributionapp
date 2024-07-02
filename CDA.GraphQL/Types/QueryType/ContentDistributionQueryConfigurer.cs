using CDA.GraphQL.Queries;
using CDA.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types.QueryType
{
    public static class ContentDistributionQueryConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field("contentDistribution")
                .Name("contentDistribution")
                .Description("the contentDistribution")
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the contentDistribution to find."))
                .Type<AssetType>()
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<string>("id");
                    if (Guid.TryParse(id, out Guid guidId))
                    {
                        var contentDistributionManager = context.Service<IContentDistributionManager>();
                        return await contentDistributionManager.GetById(guidId);
                    }
                    throw new Exception("Invalid input parameters");
                });
        }
    }



}
