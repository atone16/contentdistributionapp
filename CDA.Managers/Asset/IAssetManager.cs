using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public interface IAssetManager
    {
        Task<AssetDto> CreateAsset(AssetInput input);
        Task<AssetDto> GetById(Guid assetId);
        Task<bool> ArchiveAsset(Guid assetId);
        Task<bool> UnArchiveAsset(Guid assetId);
        Task<AssetDto> UpdateAsset(Guid assetId, AssetInput input);
    }
}
