using CDA.Data;
using CDAEnum = CDA.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class AssetType : ObjectType<AssetDto>
    {
        protected override void Configure(IObjectTypeDescriptor<AssetDto> descriptor)
        {
            descriptor.Field(x => x.Id);
            descriptor.Field(x => x.TenantId);
            descriptor.Field(x => x.AssetStatus).Type<EnumType<CDAEnum.Status>>();
        }
    }
}
