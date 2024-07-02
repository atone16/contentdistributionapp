using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public interface IContentDistributionManager
    {
        Task<bool> ArchiveContentDistribution(Guid id);
        Task<ContentDistributionDto> CreateContentDistribution(ContentDistributionInput input);
        Task<ContentDistributionDto> GetById(Guid id);
        Task<ContentDistributionDto> UpdateContentDistribution(Guid id, ContentDistributionInput input);
    }
}
