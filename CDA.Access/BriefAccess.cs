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
    public class BriefAccess : RedisCacheBaseAccess<Brief>, IBriefAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;
        private ICache cache;

        public BriefAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider)
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
            this.cache = cache;
        }

        public override async Task<Brief> CreateAsync(Brief input)
        {
            input.Id = this.guidGenerator.GenerateNewGuid();
            input.CreatedDate = dateTimeProvider.UtcNow;

            var result = await base.CreateAsync(input);

            // Add additional Logic for Brief to Order
            var briefId = $"BRIEF{result.AssetId.Replace("ASSET", "")}";
            var relationalKey = $"{typeof(Brief).Name}::{briefId}";
            await this.cache.SetAsync(relationalKey, result);

            return result;
        }

        public override Task<Brief> UpdateAsync(Brief item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return base.UpdateAsync(item);
        }

        public async Task<Brief> GetByBriefId(string briefId)
        {
            var relationalKey = $"{typeof(Brief).Name}::{briefId}";
            return await this.cache.GetAsync<Brief>(relationalKey);
        }
    }
}
