using CDA.Core;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.RedisCache
{
    public class RedisCache : ICache
    {
        private readonly Lazy<ConnectionMultiplexer> lazyConnection;

        /// <summary>
        /// Initializes a new <see cref="RedisCache"/>
        /// </summary>
        /// <param name="lazyConnection"></param>
        public RedisCache(Lazy<ConnectionMultiplexer> lazyConnection)
        {
            this.lazyConnection = lazyConnection;
        }

        private ConnectionMultiplexer Connection => this.lazyConnection.Value;

        private IDatabase Database => this.Connection.GetDatabase();

        /// <inheritdoc />
        public async Task<bool> ExistsAsync(string key)
        {
            return await this.Database.KeyExistsAsync(key);
        }

        /// <inheritdoc />
        public async Task<T> GetAsync<T>(string key)
            where T : class
        {
            return await this.Database.GetTypedAsync<T>(key);
        }

        /// <inheritdoc />
        public async Task SetAsync(string key, object item)
        {
            await this.Database.SetTypedAsync(key, item);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(string key)
        {
            return await this.Database.KeyDeleteAsync(key);
        }

        /// <inheritdoc />
        public async Task<T> HashGetAsync<T>(string hashKey, string hashField)
            where T : class
        {
            return await this.Database.HashGetTypedAsync<T>(hashKey, hashField);
        }

        /// <inheritdoc />
        public async Task<Dictionary<string, T>> HashGetAsync<T>(string hashKey, IEnumerable<string> hashFields)
            where T : class
        {
            return await this.Database.HashGetTypedAsync<T>(hashKey, hashFields.ToArray());
        }

        /// <inheritdoc />
        public async Task<List<T>> HashGetAllAsync<T>(string hashKey) where T : class
        {
            return await this.Database.HashGetAllTypedAsync<T>(hashKey);
        }

        /// <inheritdoc />
        public async Task<List<string>> HashFieldsAsync(string hashKey)
        {
            RedisValue[] keys = await this.Database.HashKeysAsync(hashKey);
            return keys.Select(key => (string)key).ToList();
        }

        /// <inheritdoc />
        public async Task HashSetAsync(string hashKey, string hashField, object item)
        {
            await this.Database.HashSetTypedAsync(hashKey, hashField, item);
        }

        /// <inheritdoc />
        public async Task HashSetAsync(string hashKey, IEnumerable<KeyValuePair<string, object>> values)
        {
            await this.Database.HashSetTypedAsync(hashKey, values);
        }

        /// <inheritdoc />
        public async Task<bool> HashDeleteAsync(string hashKey, string hashField)
        {
            return await this.Database.HashDeleteAsync(hashKey, hashField);
        }

        /// <inheritdoc />
        public async Task<long> HashDeleteAsync(string hashKey, IEnumerable<string> hashFields)
        {
            return await this.Database.HashDeleteAsync(hashKey, hashFields.ToRedisValues());
        }

        /// <inheritdoc />
        public async Task<bool> HashExistsAsync(string hashKey, string hashField)
        {
            return await this.Database.HashExistsAsync(hashKey, hashField);
        }

        /// <inheritdoc />
        public async Task DeleteAllAsync()
        {
            await this.Connection.GetServer(this.Connection.GetEndPoints()[0]).FlushDatabaseAsync(this.Database.Database);
        }
    }
}
