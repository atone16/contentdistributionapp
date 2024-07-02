using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class UserInputType : InputObjectType<UserInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<UserInput> descriptor)
        {
            descriptor.Name("UserInput");
            descriptor.Description("Input argument for user to create.");

            descriptor.Field(x => x.FirstName);
            descriptor.Field(x => x.LastName);
            descriptor.Field(x => x.Email);
        }
    }
}
