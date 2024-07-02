using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public interface IBriefManager
    {
        Task<BriefDto> CreateBrief(BriefInput input);
        Task<BriefDto> UpdateBrief(Guid id, BriefInput input);
        Task<bool> ArchiveBrief(Guid id);
        Task<BriefDto> GetById(Guid id);
    }
}
