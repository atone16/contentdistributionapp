﻿using CDA.Data;
using CDA.GraphQL.Queries;
using CDA.IManagers;

namespace CDA.GraphQL.Types.QueryType
{
    public static class BriefQueryConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field("brief")
                .Name("brief")
                .Description("the brief")
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the brief to find."))
                .Type<BriefType>()
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<string>("id");
                    if (Guid.TryParse(id, out Guid guidId))
                    {
                        var briefManager = context.Service<IBriefManager>();
                        return await briefManager.GetById(guidId);
                    }
                    throw new Exception("Invalid input parameters");
                });
        }
    }
}
