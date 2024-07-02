using CDA.Core;
using CDA.Data;
using CDA.IAccess;
using CDA.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.RedisCache
{
    public abstract class RedisCacheBaseAccess<TItem> : IBaseAccess<TItem>
        where TItem : BaseData
    {
        private readonly ICache cache;
        private readonly IGuidGenerator guidGenerator;
        private readonly IDateTimeProvider dateTimeProvider;

        public RedisCacheBaseAccess(
            ICache cache,
            IGuidGenerator guidGenerator,
            IDateTimeProvider dateTimeProvider)
        {
            this.cache = cache;
            this.guidGenerator = guidGenerator;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<TItem> CreateAsync(TItem input)
        {
            if (!await this.cache.ExistsAsync(BuildRedisKey(input)))
            {
                try
                {
                    input.Id = this.guidGenerator.GenerateNewGuid();
                    input.CreatedDate = this.dateTimeProvider.UtcNow;

                    await this.cache.SetAsync(BuildRedisKey(input), input);
                    return input;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to insert item. {ex.Message}");
                }
            }
            
            throw new Exception($"Data with Id {input.Id} already exists.");
        }

        private string BuildRedisKey(TItem input)
        {
            return $"{typeof(TItem)}::{input.Id}";
        }

        private string BuildRedisKeyById(Guid itemId)
        {
            return $"{typeof(TItem)}::{itemId}"; 
        }

        public async Task<TItem> GetByIdAsync(Guid itemId)
        {
            if(await this.cache.ExistsAsync(BuildRedisKeyById(itemId)))
            {
                var item = await this.cache.GetAsync<TItem>(BuildRedisKeyById(itemId));
                return item;
            }
            return null;
        }

        public async Task<bool> RemoveAsync(Guid itemId)
        {
            if (await this.cache.ExistsAsync(BuildRedisKeyById(itemId)))
            {
                return await this.cache.DeleteAsync(BuildRedisKeyById(itemId));
            }

            throw new Exception($"Data does not exist {itemId}");
        }

        public async Task<TItem> UpdateAsync(TItem item)
        {
            if (await this.cache.ExistsAsync(BuildRedisKey(item)))
            {
                item.LastUpdatedDate = this.dateTimeProvider.UtcNow;
                await this.cache.SetAsync(BuildRedisKey(item), item);
                return item;
            }

            throw new Exception($"Data does not exist");
        }

        public async Task<List<TItem>> GetManyAsync(List<Guid> itemIds)
        {
            return new List<TItem>();
        }

        public async Task<List<TItem>> CreateManyAsync(List<TItem> inputs)
        {
            return new List<TItem>();
        }

        public async Task<bool> RemoveManyAsync(List<TItem> inputs)
        {
            await Task.Delay(10);
            return false;
        }
    }
}
