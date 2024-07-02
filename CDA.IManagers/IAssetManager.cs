using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IManagers
{
    public interface IAssetManager
    {
        Task<AssetDto> CreateAsset(AssetInput input);
        Task<AssetDto> GetById(Guid assetId);
        Task<AssetDto> GetByAssetId(string assetId);
        Task<bool> ArchiveAsset(Guid assetId, Guid tenantId, Guid userId);
        Task<AssetDto> UpdateAsset(Guid assetId, AssetUpdateInput input);
        Task<List<AssetDto>> GetByTenantId(Guid tenantId);
        Task<List<AssetCommentDto>> GetByAssetId(Guid assetId, Guid tenantId);
        Task<AssetCommentDto> AddAssetComment(AssetCommentInput input);
        Task<bool> RemoveAssetComment(Guid id, Guid tenantId, Guid userId);
        Task<AssetCommentDto> UpdateAssetComment(Guid id, AssetCommentInput input);
        Task<AssetDto> RemoveAssetVersion(Guid assetId, Guid userId, long assetVersionNumber);
        Task<AssetDto> AddNewAssetVersion(AssetVersionInput input);
    }
}
