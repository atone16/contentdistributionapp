using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IAccess
{
    public interface IBriefCommentAccess : IBaseAccess<BriefComment>
    {
        Task<List<BriefComment>> GetByBriefId(Guid briefGuid, Guid tenantId);
    }
}
