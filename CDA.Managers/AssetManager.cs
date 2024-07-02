using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using CDA.IManagers;

namespace CDA.Managers
{
    public class AssetManager : IAssetManager
    {
        private readonly IAssetAccess assetAccess;
        private readonly IAssetCommentAccess assetCommentAccess;
        private readonly IMapper mapper;

        public AssetManager(IAssetAccess assetAccess, IAssetCommentAccess assetCommentAccess,  IMapper mapper) 
        {
            this.assetAccess = assetAccess;
            this.assetCommentAccess = assetCommentAccess;
            this.mapper = mapper;
        }

        public async Task<AssetDto> CreateAsset(AssetInput input)
        {
            var inputAsset = this.mapper.Map<Asset>(input);

            var assetVersion = new AssetVersion
            {
                Description = input.Description,
                FilePath = input.FilePath,
                Format = input.Format.Value,
                Name = input.Name,
                Preview = input.Preview,
                Number = 1,
                Type = input.Type.Value
            };

            inputAsset.CreatedBy = input.UserId;
            inputAsset.AssetVersions.Add(assetVersion);
            var createdAsset = await this.assetAccess.CreateAsync(inputAsset);
            return this.mapper.Map<AssetDto>(createdAsset);
        }

        public async Task<AssetDto> UpdateAsset(Guid assetId, AssetUpdateInput input)
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
                asset.AssignedUserId = input.AssignedUserId;
                asset.LastUpdatedBy = input.UserId;

                var updatedAsset = await this.assetAccess.UpdateAsync(asset);
                return this.mapper.Map<AssetDto>(updatedAsset);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Asset. {assetId}. {ex.Message}");
            }
        }

        public async Task<bool> ArchiveAsset(Guid assetId, Guid tenantId, Guid userId)
        {
            try
            {
                var asset = await this.assetAccess.GetByIdAsync(assetId);

                if (asset.TenantId != tenantId)
                {
                    throw new Exception("Cant archive a user not within the same tenant");
                }

                asset.IsArchived = true;
                asset.LastUpdatedBy = userId;

                await this.assetAccess.UpdateAsync(asset);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error archiving asset. {assetId}. {ex.Message}");
            }
        }

        public async Task<AssetDto> GetById(Guid assetId)
        {
            var asset = await this.assetAccess.GetByIdAsync(assetId);
            return this.mapper.Map<AssetDto>(asset);
        }

        public async Task<AssetDto> GetByAssetId(string assetId)
        {
            var asset = await this.assetAccess.GetByAssetId(assetId);
            return this.mapper.Map<AssetDto>(asset);
        }

        public async Task<List<AssetDto>> GetByTenantId(Guid tenantId)
        {
            var assets = await this.assetAccess.GetByTenantId(tenantId);
            return this.mapper.Map<List<AssetDto>>(assets);
        }

        public async Task<List<AssetCommentDto>> GetByAssetId(Guid assetId, Guid tenantId)
        {
            var assetComments = await this.assetCommentAccess.GetByAssetId(assetId, tenantId);
            return this.mapper.Map<List<AssetCommentDto>>(assetComments);
        }

        public async Task<AssetCommentDto> AddAssetComment(AssetCommentInput input)
        {
            var inputAssetComment = this.mapper.Map<AssetComment>(input);
            inputAssetComment.CreatedBy = input.UserId;
            var createdAssetComment = await this.assetCommentAccess.CreateAsync(inputAssetComment);
            return this.mapper.Map<AssetCommentDto>(createdAssetComment);
        }

        public async Task<bool> RemoveAssetComment(Guid id, Guid tenantId, Guid userId)
        {
            try
            {
                var assetComment = await this.assetCommentAccess.GetByIdAsync(id);
                if (assetComment.TenantId != tenantId)
                {
                    throw new Exception("Cannot Delete AssetComment if not same tenant");
                }

                if (assetComment.CreatedBy != userId)
                {
                    throw new Exception("Cannot update AssetComment that is not created by the user.");
                }

                return await this.assetCommentAccess.RemoveAsync(id, assetComment.TenantId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting record. {ex.Message}");
            }
        }

        public async Task<AssetCommentDto> UpdateAssetComment(Guid id, AssetCommentInput input)
        {
            try
            {
                var assetComment = await this.assetCommentAccess.GetByIdAsync(id);

                if (assetComment == null)
                {
                    throw new Exception("Cannot update non existent asset comment.");
                }

                if (assetComment.IsArchived)
                {
                    throw new Exception("Cannot update an archived asset comment.");
                }

                if (assetComment.CreatedBy != input.UserId)
                {
                    throw new Exception("Cannot update a comment that is not created by the user.");
                }

                assetComment.Comment = input.Comment;
                assetComment.LastUpdatedBy = input.UserId;

                var updatedAssetComment = await this.assetCommentAccess.UpdateAsync(assetComment);
                return this.mapper.Map<AssetCommentDto>(updatedAssetComment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Asset Comment. {id}. {ex.Message}");
            }
        }

        public async Task<AssetDto> AddNewAssetVersion(AssetVersionInput input)
        {
            try
            {
                var asset = await this.assetAccess.GetByIdAsync(input.AssetGuid);

                var currentVersionCount = asset.AssetVersions.Count();
                asset.AssetVersions.Add(new AssetVersion
                {
                    Description = input.Description,
                    FilePath = input.FilePath,
                    Format = input.Format,
                    Name = input.Name,
                    Preview = input.Preview,
                    Size = input.Size,
                    Type = input.Type,
                    Number = currentVersionCount + 1
                });

                asset.LastUpdatedBy = input.UserId;
                var updatedAsset = await this.assetAccess.UpdateAsync(asset);
                return this.mapper.Map<AssetDto>(updatedAsset);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Asset Version. {input.AssetGuid}. {ex.Message}");
            }
        }

        public async Task<AssetDto> RemoveAssetVersion(Guid assetId, Guid userId, long assetVersionNumber)
        {
            try
            {
                var asset = await this.assetAccess.GetByIdAsync(assetId);

                asset.AssetVersions = asset.AssetVersions.Where(x => x.Number != assetVersionNumber).ToList();
                asset.LastUpdatedBy = userId;

                var updatedAsset = await this.assetAccess.UpdateAsync(asset);
                return this.mapper.Map<AssetDto>(updatedAsset);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Asset Version. {assetId}. {ex.Message}");
            }
        }
    }
}
