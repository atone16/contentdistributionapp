using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class AssetCommentType : ObjectType<AssetCommentDto>
    {
        protected override void Configure(IObjectTypeDescriptor<AssetCommentDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.Comment);
            descriptor.Field(x => x.CreatedBy);
        }
    }
}
