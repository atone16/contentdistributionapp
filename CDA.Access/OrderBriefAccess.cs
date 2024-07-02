using CDA.Core;
using CDA.Data;
using CDA.IAccess;
using CDA.RedisCache;
using CDA.Utilities;

namespace CDA.Access
{
    public class OrderBriefAccess : RedisCacheBaseAccess<OrderBrief>, IOrderBriefAccess
    {
        private IGuidGenerator guidGenerator;
        private IDateTimeProvider dateTimeProvider;
        private ICache cache;

        public OrderBriefAccess(ICache cache, IGuidGenerator guidGenerator, IDateTimeProvider dateTimeProvider)
            : base(cache)
        {
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
            this.cache = cache;
        }

        public override async Task<OrderBrief> CreateAsync(OrderBrief input)
        {
            input.Id = this.guidGenerator.GenerateNewGuid();
            input.CreatedDate = dateTimeProvider.UtcNow;

            var result = await base.CreateAsync(input);

            // Add additional Logic to create relations
            var relationalKey = $"{result.OrderId}::{typeof(OrderBrief).Name}";
            await this.cache.SetAddAsync(relationalKey, result.Id);

            return result;
        }

        public override Task<OrderBrief> UpdateAsync(OrderBrief item)
        {
            item.LastUpdatedDate = dateTimeProvider.UtcNow;
            return base.UpdateAsync(item);
        }

        public override async Task<bool> RemoveAsync(Guid itemId, Guid tenantId)
        {
            var objectKey = $"{typeof(OrderBrief).Name}::{itemId}";
            var orderBrief = await this.cache.GetAsync<OrderBrief>(objectKey);

            var result = await base.RemoveAsync(itemId, tenantId);

            var relationalKey = $"{orderBrief.OrderId}::{typeof(OrderBrief).Name}";
            await this.cache.SetRemoveAsync(relationalKey, $"\"{itemId.ToString()}\"");

            return result;
        }

        public async Task<List<OrderBrief>> GetByOrderId(Guid orderId, Guid tenantId)
        {
            return await this.cache.GetItemByKey<OrderBrief>(orderId.ToString());
        }
    }
}
