using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddCDAGraphQL(this IServiceCollection services)
        {
            services
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
                })
                .AddGraphQLServer()
                .AddGraphQLTypes()
                .AddAuthorization()
                .ModifyOptions(o => o.RemoveUnreachableTypes = true)
                .BindRuntimeType<DateTime, DateTimeType>()// // this will force the datetime/datetime offset used for dates to return dates
                .AddMaxExecutionDepthRule(15)
                .SetRequestOptions(o => new HotChocolate.Execution.Options.RequestExecutorOptions
                {
                    ExecutionTimeout = TimeSpan.FromSeconds(300)
                });

            return services;
        }
    }
}
