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
    public class BriefCommentAccess : RedisCacheBaseAccess<BriefComment>, IBriefCommentAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;
        private ICache cache;

        public BriefCommentAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider)
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
            this.cache = cache;
        }

        public override async Task<BriefComment> CreateAsync(BriefComment input)
        {
            input.Id = this.guidGenerator.GenerateNewGuid();
            input.CreatedDate = dateTimeProvider.UtcNow;

            var result = await base.CreateAsync(input);

            // Add additional Logic to create relations
            var relationalKey = $"{result.BriefGuid}::{typeof(BriefComment).Name}";
            await this.cache.SetAddAsync(relationalKey, result.Id);

            return result;
        }

        public override Task<BriefComment> UpdateAsync(BriefComment item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return base.UpdateAsync(item);
        }

        public override async Task<bool> RemoveAsync(Guid itemId, Guid tenantId)
        {
            var objectKey = $"{typeof(BriefComment).Name}::{itemId}";
            var briefComment = await this.cache.GetAsync<BriefComment>(objectKey);

            var result = await base.RemoveAsync(itemId, tenantId);

            var relationalKey = $"{briefComment.BriefGuid}::{typeof(BriefComment).Name}";
            await this.cache.SetRemoveAsync(relationalKey, $"\"{itemId.ToString()}\"");

            return result;
        }

        public async Task<List<BriefComment>> GetByBriefId(Guid briefGuid, Guid tenantId)
        {
            return await this.cache.GetItemByKey<BriefComment>(briefGuid.ToString());
        }
    }
}
