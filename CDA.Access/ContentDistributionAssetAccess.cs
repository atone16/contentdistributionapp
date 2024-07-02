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
    public class ContentDistributionAssetAccess : RedisCacheBaseAccess<ContentDistributionAsset>, IContentDistributionAssetAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;
        private ICache cache;

        public ContentDistributionAssetAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider)
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
            this.cache = cache;
        }

        public override async Task<ContentDistributionAsset> CreateAsync(ContentDistributionAsset input)
        {
            input.Id = this.guidGenerator.GenerateNewGuid();
            input.CreatedDate = dateTimeProvider.UtcNow;

            var result = await base.CreateAsync(input);

            // Add additional Logic to create relations
            var relationalKey = $"{result.ContentDistributionId}::{typeof(ContentDistributionAsset).Name}";
            await this.cache.SetAddAsync(relationalKey, result.Id);

            return result;
        }

        public override Task<ContentDistributionAsset> UpdateAsync(ContentDistributionAsset item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return base.UpdateAsync(item);
        }

        public override async Task<bool> RemoveAsync(Guid itemId, Guid tenantId)
        {
            var objectKey = $"{typeof(ContentDistributionAsset).Name}::{itemId}";
            var contentDistributionAsset = await this.cache.GetAsync<ContentDistributionAsset>(objectKey);

            var result = await base.RemoveAsync(itemId, tenantId);

            var relationalKey = $"{contentDistributionAsset.ContentDistributionId}::{typeof(ContentDistributionAsset).Name}";
            await this.cache.SetRemoveAsync(relationalKey, $"\"{itemId.ToString()}\"");

            return result;
        }

        public async Task<List<ContentDistributionAsset>> GetByContentDistributionId(Guid contentDistributionId)
        {
            return await this.cache.GetItemByKey<ContentDistributionAsset>(contentDistributionId.ToString());
        }

    }
}
