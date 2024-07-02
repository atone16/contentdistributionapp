using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IManagers
{
    public interface IContentDistributionManager
    {
        Task<bool> ArchiveContentDistribution(Guid id, Guid tenantId, Guid userId);
        Task<ContentDistributionDto> CreateContentDistribution(ContentDistributionInput input);
        Task<ContentDistributionDto> GetById(Guid id);
        Task<ContentDistributionDto> UpdateContentDistribution(Guid id, ContentDistributionInput input);
        Task<List<ContentDistributionDto>> GetByTenantId(Guid tenantId);
        Task<ContentDistributionAssetDto> AddContentDistributionAsset(ContentDistributionAssetInput input);
        Task<bool> RemoveContentDistributionAsset(Guid id, Guid tenantId, Guid userId);
        Task<ContentDistributionAssetDto> UpdateContentDistributionAsset(Guid id, ContentDistributionAssetInput input);
        Task<List<ContentDistributionAssetDto>> GetByContentDistribution(Guid contentDistributionId, Guid tenantId);
    }
}
