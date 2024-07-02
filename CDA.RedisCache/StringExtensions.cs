using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.RedisCache
{
    public static class StringExtensions
    {
        /// <summary>
        /// Gets the given strings as Redis values.
        /// </summary>
        /// <param name="strings">Strings to convert to Redis values.</param>
        /// <returns>Redis values for strings.</returns>
        public static RedisValue[] ToRedisValues(this IEnumerable<string> strings)
        {
            return strings.Select(key => (RedisValue)key).ToArray();
        }
    }
}
