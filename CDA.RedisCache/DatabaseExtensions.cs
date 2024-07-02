using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.RedisCache
{
    /// <summary>
    /// Provides extensions for the Redis <see cref="IDatabase"/> interface.
    /// </summary>
    public static class DatabaseExtensions
    {
        private const string RedisExceptionMessageForNullValue = "A null value is not valid in this context";

        /// <summary>
        /// Retrieves and decompresses the object of type <typeparamref name="T"/> with the given key.
        /// </summary>
        /// <typeparam name="T">Type of object to retrieve.</typeparam>
        /// <param name="db">Redis database.</param>
        /// <param name="key">Key of object to retrieve.</param>
        /// <returns>Object decompressed and deserialized.</returns>
        public static async Task<T> GetTypedAsync<T>(this IDatabase db, string key)
        {
            string cacheData = await db.StringGetAsync(key);
            return cacheData == null
                ? default(T)
                : JsonDeserialize<T>(cacheData);
        }

        /// <summary>
        /// Compresses an object and stores it in the cache.
        /// </summary>
        /// <param name="db">Redis database.</param>
        /// <param name="key">Key to give the object in the cache.</param>
        /// <param name="value">The value to compress and store.</param>
        public static async Task SetTypedAsync(this IDatabase db, string key, object value)
        {
            try
            {
                await db.StringSetAsync(key, JsonSerialize(value));
            }
            catch (Exception ex)
            {
                if (!IsRedisNullValueException(ex))
                {
                    throw;
                }

                throw new ArgumentException(
                    $"Attempted to set null string value in cache for key \"{key}\".",
                    ex);
            }
        }

        public static async Task SetAddTypedAsync(this IDatabase db, string key, object value)
        {
            try
            {
                await db.SetAddAsync(key, JsonSerialize(value));
            }
            catch (Exception ex)
            {
                if (!IsRedisNullValueException(ex))
                {
                    throw;
                }

                throw new ArgumentException(
                    $"Attempted to set null string value in cache for key \"{key}\".",
                    ex);
            }
        }

        private static bool IsRedisNullValueException(Exception ex)
        {
            return ex.Message.Contains(RedisExceptionMessageForNullValue);
        }

        private static string JsonSerialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        private static T JsonDeserialize<T>(string o)
        {
            return JsonConvert.DeserializeObject<T>(o);
        }
    }

}
