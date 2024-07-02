using CDA.GraphQL.Mutations;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL
{
    public class MutationType : ObjectType<Mutation>
    {
        protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            ConfigureMutations(descriptor);
        }

        private static void ConfigureMutations(IObjectTypeDescriptor<Mutation> descriptor)
        {
            // Invoke each configurer's Configure() method to add the mutations that configurer is responsible for.
            // We split up the mutations this way because HC doesn't (currently) allow multiple root mutation types
            // and this saves us from having a giant Configure() method in MutationType.
            foreach (TypeInfo configurerType in TypeUtilities.GetMutationConfigurerTypes())
            {
                configurerType.GetMethod("Configure")?.Invoke(null, new object[] { descriptor });
            }
        }
    }
}
