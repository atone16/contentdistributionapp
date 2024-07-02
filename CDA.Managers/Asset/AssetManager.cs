using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public class AssetManager : IAssetManager
    {
        private readonly IAssetAccess assetAccess;
        private readonly IMapper mapper;

        public AssetManager(IAssetAccess assetAccess, IMapper mapper) 
        {
            this.assetAccess = assetAccess;
            this.mapper = mapper;
        }

        public async Task<AssetDto> CreateAsset(AssetInput input)
        {
            var inputAsset = this.mapper.Map<Asset>(input);
            var createdAsset = await this.assetAccess.CreateAsync(inputAsset);
            return this.mapper.Map<AssetDto>(createdAsset);
        }

        public async Task<AssetDto> UpdateAsset(Guid assetId, AssetInput input)
        {
            try
            {
                var asset = await this.assetAccess.GetByIdAsync(assetId);
                
                if (asset == null)
                {
                    throw new Exception("Cannot update non existent asset.");
                }

                if (asset.IsArchived) 
                {
                    throw new Exception("Cannot update an archived asset.");
                }

                asset.AssetStatus = input.AssetStatus;
                var updatedAsset = await this.assetAccess.UpdateAsync(asset);
                return this.mapper.Map<AssetDto>(updatedAsset);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Asset. {assetId}. {ex.Message}");
            }
        }

        public async Task<bool> ArchiveAsset(Guid assetId)
        {
            try
            {
                var asset = await this.assetAccess.GetByIdAsync(assetId);
                asset.IsArchived = true;
                await this.assetAccess.UpdateAsync(asset);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error archiving asset. {assetId}. {ex.Message}");
            }
        }

        public async Task<bool> UnArchiveAsset(Guid assetId)
        {
            try
            {
                var asset = await this.assetAccess.GetByIdAsync(assetId);
                asset.IsArchived = false;
                await this.assetAccess.UpdateAsync(asset);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error unarchiving asset. {assetId}. {ex.Message}");
            }
        }

        public async Task<AssetDto> GetById(Guid assetId)
        {
            var asset = await this.assetAccess.GetByIdAsync(assetId);
            return this.mapper.Map<AssetDto>(asset);
        }

        //public async Task<List<AssetDto>> GetAssetsByTenantId(Guid tenantId)
        //{
        //    var asset = await this.assetAccess.GetByIdAsync(assetId);
        //    return this.mapper.Map<AssetDto>(asset);
        //}

    }
}
