using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using CDA.IManagers;

namespace CDA.Managers
{
    public class ContentDistributionManager : IContentDistributionManager
    {
        private readonly IContentDistributionAccess contentDistributionAccess;
        private readonly IContentDistributionAssetAccess contentDistributionAssetAccess;

        private readonly IMapper mapper;

        public ContentDistributionManager(
            IContentDistributionAccess contentDistributionAccess,
            IContentDistributionAssetAccess contentDistributionAssetAccess,
            IMapper mapper
            ) 
        { 
            this.contentDistributionAccess = contentDistributionAccess;
            this.contentDistributionAssetAccess = contentDistributionAssetAccess;
            this.mapper = mapper;
        }

        public async Task<bool> ArchiveContentDistribution(Guid id, Guid tenantId, Guid userId)
        {
            try
            {
                var contentDistribution = await this.contentDistributionAccess.GetByIdAsync(id);

                if (contentDistribution.TenantId != tenantId)
                {
                    throw new Exception("Cant archive a content distribution not within the same tenant");
                }

                contentDistribution.IsArchived = true;
                contentDistribution.LastUpdatedBy = userId;

                await this.contentDistributionAccess.UpdateAsync(contentDistribution);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error Archiving ContentDistribution. {id}. {ex.Message}");
            }
        }

        public async Task<ContentDistributionDto> CreateContentDistribution(ContentDistributionInput input)
        {
            var inputContentDistribution = this.mapper.Map<ContentDistribution>(input);
            inputContentDistribution.CreatedBy = input.UserId;
            var createdContentDistribution = await this.contentDistributionAccess.CreateAsync(inputContentDistribution);
            return this.mapper.Map<ContentDistributionDto>(createdContentDistribution);
        }

        public async Task<ContentDistributionDto> GetById(Guid id)
        {
            var contentDistribution = await this.contentDistributionAccess.GetByIdAsync(id);
            return this.mapper.Map<ContentDistributionDto>(contentDistribution);
        }

        public async Task<ContentDistributionDto> UpdateContentDistribution(Guid id, ContentDistributionInput input)
        {
            try
            {
                var contentDistribution = await this.contentDistributionAccess.GetByIdAsync(id);

                if (contentDistribution == null)
                {
                    throw new Exception("Cannot Update A NonExistent ContentDistribution.");
                }

                if (contentDistribution.IsArchived)
                {
                    throw new Exception("Cannot Update An Archived ContentDistribution.");
                }

                contentDistribution.DistributionChannels = input.DistributionChannels;
                contentDistribution.DistributionMethods = input.DistributionMethods;
                contentDistribution.DistributionDate = input.DistributionDate;
                contentDistribution.LastUpdatedBy = input.UserId;

                var updatedContentDistribution = await this.contentDistributionAccess.UpdateAsync(contentDistribution);
                return this.mapper.Map<ContentDistributionDto>(updatedContentDistribution);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating ContentDistribution. {id}. {ex.Message}");
            }
        }

        public async Task<List<ContentDistributionDto>> GetByTenantId(Guid tenantId)
        {
            var contentDistributions = await this.contentDistributionAccess.GetByTenantId(tenantId);
            return this.mapper.Map<List<ContentDistributionDto>>(contentDistributions);
        }

        public async Task<ContentDistributionAssetDto> AddContentDistributionAsset(ContentDistributionAssetInput input)
        {
            var inputContentDistributionAsset = this.mapper.Map<ContentDistributionAsset>(input);
            inputContentDistributionAsset.CreatedBy = input.UserId;
            var createdAssetComment = await this.contentDistributionAssetAccess.CreateAsync(inputContentDistributionAsset);
            return this.mapper.Map<ContentDistributionAssetDto>(inputContentDistributionAsset);
        }

        public async Task<bool> RemoveContentDistributionAsset(Guid id, Guid tenantId, Guid userId)
        {
            try
            {
                var contentDistributionAsset = await this.contentDistributionAssetAccess.GetByIdAsync(id);
                if (contentDistributionAsset.TenantId != tenantId)
                {
                    throw new Exception("Cannot Delete ContentDistributionAsset if not same tenant");
                }

                return await this.contentDistributionAssetAccess.RemoveAsync(id, contentDistributionAsset.TenantId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting record. {ex.Message}");
            }
        }

        public async Task<ContentDistributionAssetDto> UpdateContentDistributionAsset(Guid id, ContentDistributionAssetInput input)
        {
            try
            {
                var contentDistributionAsset = await this.contentDistributionAssetAccess.GetByIdAsync(id);

                if (contentDistributionAsset == null)
                {
                    throw new Exception("Cannot update non existent content distribution asset.");
                }

                if (contentDistributionAsset.IsArchived)
                {
                    throw new Exception("Cannot update an archived content distribution asset.");
                }

                contentDistributionAsset.FileUrl = input.FileUrl ?? contentDistributionAsset.FileUrl;
                contentDistributionAsset.Name = input.Name ?? contentDistributionAsset.Name;
                contentDistributionAsset.AssetId = input.AssetId ?? contentDistributionAsset.AssetId;

                var updatedContentDistributionAsset = await this.contentDistributionAssetAccess.UpdateAsync(contentDistributionAsset);
                return this.mapper.Map<ContentDistributionAssetDto>(updatedContentDistributionAsset);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Asset Comment. {id}. {ex.Message}");
            }
        }

        public async Task<List<ContentDistributionAssetDto>> GetByContentDistribution(Guid contentDistributionId, Guid tenantId)
        {
            var contentDistributionAssets = await this.contentDistributionAssetAccess.GetByContentDistributionId(contentDistributionId);
            return this.mapper.Map<List<ContentDistributionAssetDto>>(contentDistributionAssets);
        }

    }
}
