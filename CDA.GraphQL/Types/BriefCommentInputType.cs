using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types
{
    public class BriefCommentInputType : InputObjectType<BriefCommentInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<BriefCommentInput> descriptor)
        {
            descriptor.Name("BriefCommentInput");
            descriptor.Description("Input argument for brief comments");

            descriptor.Field(x => x.BriefGuid);
            descriptor.Field(x => x.Comment);

            descriptor.Ignore(x => x.TenantId);
            descriptor.Ignore(x => x.UserId);
        }
    }
}
