using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class UserType : ObjectType<UserDto>
    {
        protected override void Configure(IObjectTypeDescriptor<UserDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.FirstName);
            descriptor.Field(x => x.LastName);
            descriptor.Field(x => x.Email);
        }
    }
}
