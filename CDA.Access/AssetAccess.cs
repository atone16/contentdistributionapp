using CDA.Core;
using CDA.Data;
using CDA.IAccess;
using CDA.RedisCache;
using CDA.Utilities;
using System.Security.Cryptography.X509Certificates;

namespace CDA.Access
{
    public class AssetAccess : RedisCacheBaseAccess<Asset>, IAssetAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;
        private ICache cache;

        public AssetAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider) 
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
            this.cache = cache;
        }

        public override async Task<Asset> CreateAsync(Asset input)
        {
            input.Id = this.guidGenerator.GenerateNewGuid();
            input.CreatedDate = dateTimeProvider.UtcNow;
            var result = await base.CreateAsync(input);

            // Add additional Logic to create relations
            var relationalKey = $"{typeof(Asset).Name}::{result.AssetId}";
            await this.cache.SetAsync(relationalKey, result);

            return result;
        }

        public override async Task<Asset> UpdateAsync(Asset item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return await base.UpdateAsync(item);
        }

        public async Task<Asset> GetByAssetId(string assetId)
        {
            var relationalKey = $"{typeof(Asset).Name}::{assetId}";
            return await this.cache.GetAsync<Asset>(relationalKey);
        }
    }
}
