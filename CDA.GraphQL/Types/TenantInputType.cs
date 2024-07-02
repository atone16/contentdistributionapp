using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class TenantInputType : InputObjectType<TenantInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<TenantInput> descriptor)
        {
            descriptor.Name("TenantInput");
            descriptor.Description("Input argument for tenant to create.");

            descriptor.Field(x => x.TenantName);
        }
    }
}
