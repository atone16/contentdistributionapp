using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IManagers
{
    public interface IBriefManager
    {
        Task<BriefDto> CreateBrief(BriefInput input);
        Task<BriefDto> UpdateBrief(Guid id, BriefInput input);
        Task<bool> ArchiveBrief(Guid id, Guid tenantId, Guid userId);
        Task<BriefDto> GetById(Guid id);
        Task<BriefDto> GetByBriefId(string briefId);
        Task<List<BriefDto>> GetByTenantId(Guid tenantId);
        Task<BriefCommentDto> AddBriefComment(BriefCommentInput input);
        Task<bool> RemoveBriefComment(Guid id, Guid tenantId, Guid userId);
        Task<BriefCommentDto> UpdateBriefComment(Guid id, BriefCommentInput input);
        Task<List<BriefCommentDto>> GetByBriefId(Guid briefId, Guid tenantId);

    }
}
