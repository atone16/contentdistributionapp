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
        /// Retrieves and decompresses the object of type <typeparamref name="T"/> with the given hash field from the given hash.
        /// </summary>
        /// <typeparam name="T">Type of object to retrieve.</typeparam>
        /// <param name="db">Redis database.</param>
        /// <param name="hashKey">Key of Redis hash structure containing the object.</param>
        /// <param name="hashField">Field containing the object within the hash.</param>
        /// <returns>Object decompressed and deserialized.</returns>
        public static async Task<T> HashGetTypedAsync<T>(this IDatabase db, string hashKey, string hashField)
        {
            byte[] compressedBytes = await db.HashGetAsync(hashKey, hashField);
            return compressedBytes == null
                ? default(T)
                : DecompressAndDeserialize<T>(compressedBytes);
        }

        /// <summary>
        /// Retrieves and decompresses the objects of type <typeparamref name="T"/> with the given hash field from the given hash.
        /// </summary>
        /// <typeparam name="T">Type of objects to retrieve.</typeparam>
        /// <param name="db">Redis database.</param>
        /// <param name="hashKey">Key of Redis hash structure containing the object.</param>
        /// <param name="hashFields">Fields containing the objects within the hash.</param>
        /// <returns>Lookup by hash field of object decompressed and deserialized.</returns>
        public static async Task<Dictionary<string, T>> HashGetTypedAsync<T>(
            this IDatabase db,
            string hashKey,
            string[] hashFields)
        {
            RedisValue[] fieldValues = await db.HashGetAsync(hashKey, hashFields.ToRedisValues());
            var result = new Dictionary<string, T>();
            for (int i = 0; i < hashFields.Length; i++)
            {
                string fieldName = hashFields[i];
                byte[] fieldValueCompressedBytes = fieldValues[i];
                T fieldValue = fieldValueCompressedBytes == null
                    ? default(T)
                    : DecompressAndDeserialize<T>(fieldValueCompressedBytes);
                result.Add(fieldName, fieldValue);
            }

            return result;
        }

        /// <summary>
        /// Compresses an object and stores it in a Redis hash field.
        /// </summary>
        /// <param name="db">Redis database.</param>
        /// <param name="hashKey">Key of Redis hash structure in which to store the object.</param>
        /// <param name="hashField">Field to give the object in the hash.</param>
        /// <param name="value">The value to compress and store.</param>
        public static async Task HashSetTypedAsync(this IDatabase db, string hashKey, string hashField, object value)
        {
            byte[] compressedBytes = SerializeAndCompress(value);
            try
            {
                await db.HashSetAsync(hashKey, hashField, compressedBytes);
            }
            catch (Exception ex)
            {
                if (!IsRedisNullValueException(ex))
                {
                    throw;
                }

                throw new ArgumentException(
                    $"Attempted to set null hash value in cache for hash key \"{hashKey}\" and hash field \"{hashField}\".",
                    ex);
            }
        }

        /// <summary>
        /// Compresses multiple objects and stores each in a Redis hash.
        /// </summary>
        /// <param name="db">Redis database.</param>
        /// <param name="hashKey">Key of Redis hash structure in which to store the objects.</param>
        /// <param name="values">The objects to store, as a sequence of pairs where the key of each pair is
        /// the hash field to give the object, and the value is the object to store.</param>
        public static async Task HashSetTypedAsync(
            this IDatabase db,
            string hashKey,
            IEnumerable<KeyValuePair<string, object>> values)
        {
            HashEntry[] entries = values
                .Select(val => new HashEntry(val.Key, SerializeAndCompress(val.Value)))
                .ToArray();
            try
            {
                await db.HashSetAsync(hashKey, entries);
            }
            catch (Exception ex)
            {
                if (!IsRedisNullValueException(ex))
                {
                    throw;
                }

                throw new ArgumentException(
                    $"Attempted to set null hash value in cache for hash key \"{hashKey}\". Uncompressed hash entries: {GetEntriesString(values)}",
                    ex);
            }
        }

        private static string GetEntriesString(IEnumerable<KeyValuePair<string, object>> values)
        {
            return string.Join(
                Environment.NewLine,
                values.Select(
                    pair =>
                        $"Key: {pair.Key}, Value: {pair.Value}"));
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

        private static bool IsRedisNullValueException(Exception ex)
        {
            return ex.Message.Contains(RedisExceptionMessageForNullValue);
        }

        /// <summary>
        /// Decompresses and returns all values as objects of type <typeparamref name="T"/> from the given Redis hash.
        /// </summary>
        /// <typeparam name="T">Type to deserialize objects as.</typeparam>
        /// <param name="db">Redis database.</param>
        /// <param name="hashKey">Key of hash containing the objects.</param>
        /// <returns>List of decompressed and deserialized objects.</returns>
        public static async Task<List<T>> HashGetAllTypedAsync<T>(this IDatabase db, string hashKey)
        {
            HashEntry[] entries = await db.HashGetAllAsync(hashKey);
            return entries
                .Select(
                    entry => DecompressAndDeserialize<T>(entry.Value))
                .ToList();
        }

        private static byte[] SerializeAndCompress(object o)
        {
            return o.CompressAndSerialize();
        }

        private static string JsonSerialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        private static T JsonDeserialize<T>(string o)
        {
            return JsonConvert.DeserializeObject<T>(o);
        }

        private static T DecompressAndDeserialize<T>(byte[] compressedBytes)
        {
            return compressedBytes.DecompressAndDeserialize<T>();
        }
    }

}
