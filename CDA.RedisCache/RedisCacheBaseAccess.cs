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

        public RedisCacheBaseAccess(ICache cache)
        {
            this.cache = cache;
        }

        public virtual async Task<TItem> CreateAsync(TItem input)
        {
            if (!await this.cache.ExistsAsync(BuildRedisKey(input)))
            {
                try
                {
                    await this.cache.SetAsync(BuildRedisKey(input), input);
                    await this.cache.SetAddAsync(BuildRelationalKey(input.TenantId), input.Id.ToString().Replace("\"", ""));
                    return input;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to insert item. {ex.Message}");
                }
            }
            
            throw new Exception($"Data with Id {input.Id} already exists.");
        }

        private static string BuildRelationalKey(Guid tenantId)
        {
            return $"{tenantId}::{typeof(TItem).Name}";
        }

        private string BuildRedisKey(TItem input)
        {
            return $"{typeof(TItem).Name}::{input.Id}";
        }

        private string BuildRedisKeyById(Guid itemId)
        {
            return $"{typeof(TItem).Name}::{itemId}"; 
        }

        public virtual async Task<TItem> GetByIdAsync(Guid itemId)
        {
            if(await this.cache.ExistsAsync(BuildRedisKeyById(itemId)))
            {
                var item = await this.cache.GetAsync<TItem>(BuildRedisKeyById(itemId));
                return item;
            }
            return null;
        }

        public virtual async Task<TItem> UpdateAsync(TItem item)
        {
            if (await this.cache.ExistsAsync(BuildRedisKey(item)))
            {
                await this.cache.SetAsync(BuildRedisKey(item), item);
                return item;
            }

            throw new Exception($"Data does not exist");
        }

        public virtual async Task<bool> RemoveAsync(Guid itemId, Guid tenantId)
        {
            if (await this.cache.ExistsAsync(BuildRedisKeyById(itemId)))
            {
                await this.cache.SetRemoveAsync(BuildRelationalKey(tenantId), $"\"{itemId.ToString()}\"");
                return await this.cache.DeleteAsync(BuildRedisKeyById(itemId));
            }

            throw new Exception($"Data does not exist {itemId}");
        }

        public async Task<List<TItem>> GetByTenantId(Guid tenantId)
        {
            return await this.cache.GetItemByKey<TItem>(tenantId.ToString());
        }
    }
}
