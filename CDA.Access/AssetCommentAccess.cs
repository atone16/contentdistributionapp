using CDA.Core;
using CDA.Data;
using CDA.IAccess;
using CDA.RedisCache;
using CDA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Access
{

    public class AssetCommentAccess : RedisCacheBaseAccess<AssetComment>, IAssetCommentAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;
        private ICache cache;

        public AssetCommentAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider)
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
            this.cache = cache;
        }

        public override async Task<AssetComment> CreateAsync(AssetComment input)
        {
            input.Id = this.guidGenerator.GenerateNewGuid();
            input.CreatedDate = dateTimeProvider.UtcNow;

            var result = await base.CreateAsync(input);

            // Add additional Logic to create relations
            var relationalKey = $"{result.AssetGuid}::{typeof(AssetComment).Name}";
            await this.cache.SetAddAsync(relationalKey, result.Id);

            return result;
        }

        public override Task<AssetComment> UpdateAsync(AssetComment item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return base.UpdateAsync(item);
        }

        public override async Task<bool> RemoveAsync(Guid itemId, Guid tenantId)
        {
            var objectKey = $"{typeof(AssetComment).Name}::{itemId}";
            var assetComment = await this.cache.GetAsync<AssetComment>(objectKey);

            var result = await base.RemoveAsync(itemId, tenantId);

            var relationalKey = $"{assetComment.AssetGuid}::{typeof(AssetComment).Name}";
            await this.cache.SetRemoveAsync(relationalKey, $"\"{itemId.ToString()}\"");

            return result;
        }

        public async Task<List<AssetComment>> GetByAssetId(Guid assetGuid, Guid tenantId)
        {
            return await this.cache.GetItemByKey<AssetComment>(assetGuid.ToString());
        }
    }
}
