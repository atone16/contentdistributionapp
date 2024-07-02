using CDA.Core;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CDA.RedisCache
{
    public class RedisCache : ICache
    {
        private readonly Lazy<ConnectionMultiplexer> lazyConnection;

        public RedisCache(Lazy<ConnectionMultiplexer> lazyConnection)
        {
            this.lazyConnection = lazyConnection;
        }

        private ConnectionMultiplexer Connection => this.lazyConnection.Value;

        private IDatabase Database => this.Connection.GetDatabase();

        public async Task<bool> ExistsAsync(string key)
        {
            return await this.Database.KeyExistsAsync(key);
        }

        public async Task<List<T>> GetItemByKey<T>(string key) 
            where T : class
        {
            var relationalRedisKey = new RedisKey($"{key}::{typeof(T).Name}");
            var idsToGet = await this.Database.SetMembersAsync(relationalRedisKey);
            var result = new List<T>();

            foreach (var id in idsToGet.Select(member => member.ToString().Replace("\"", "")).ToList())
            {
                var redisKey = new RedisKey($"{typeof(T).Name}::{id}");
                var item = this.Database.StringGet(redisKey);
                result.Add(JsonConvert.DeserializeObject<T>(item));
            }
            return result;
        }

        public async Task<T> GetAsync<T>(string key)
            where T : class
        {
            return await this.Database.GetTypedAsync<T>(key);
        }

        public async Task SetAsync(string key, object item)
        {
            await this.Database.SetTypedAsync(key, item);
        }

        public async Task SetRemoveAsync(string key, string itemId)
        {
            await this.Database.SetRemoveAsync(key, itemId);
        }

        public async Task SetAddAsync(string key, object item)
        {
            await this.Database.SetAddTypedAsync(key, item);
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await this.Database.KeyDeleteAsync(key);
        }
    }
}
