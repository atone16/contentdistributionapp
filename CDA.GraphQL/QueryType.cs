using CDA.GraphQL.Queries;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL
{
    public class QueryType : ObjectType<Query>
    {
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            ConfigureQueries(descriptor);
        }

        private static void ConfigureQueries(IObjectTypeDescriptor<Query> descriptor)
        {
            // Invoke each configurer's Configure() method to add the queries that configurer is responsible for.
            // We split up the queries this way because HC doesn't (currently) allow multiple root query types
            // and this saves us from having a giant Configure() method in QueryType.
            foreach (TypeInfo configurerType in TypeUtilities.GetQueryConfigurerTypes())
            {
                configurerType.GetMethod("Configure")?.Invoke(null, new object[] { descriptor });
            }
        }
    }
}
