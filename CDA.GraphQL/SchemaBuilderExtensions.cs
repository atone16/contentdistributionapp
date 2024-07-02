using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL
{
    public static class SchemaBuilderExtensions
    {
        public static IRequestExecutorBuilder AddGraphQLTypes(this IRequestExecutorBuilder builder)
        {
            return builder
                .AddQueryType<QueryType>()
                .AddMutationType<MutationType>()
                .AddTypes(TypeUtilities.GetGraphTypes());
        }
    }
}
