using CDA.Core;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.RedisCache
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers a singleton <see cref="RedisCache"/> instance for the <see cref="ICache"/> interface,
        /// using the given connection string.
        /// </summary>
        /// <param name="services">Services being configured.</param>
        /// <param name="connectionString">The Redis connection string to use.</param>
        public static IServiceCollection AddRedisCache(
            this IServiceCollection services,
            string connectionString)
        {
            services.AddSingleton(
                new Lazy<ConnectionMultiplexer>(
                    () =>
                    {
                        ConfigurationOptions options = CreateConnectionMultiplexerOptions(connectionString);
                        return ConnectionMultiplexer.Connect(options);
                    }));

            services.AddSingleton<ICache, RedisCache>();

            return services;
        }
        
        /// <summary>
        /// Create new instance of  Lazy ConnectionMultiplexer 
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns></returns>
        public static ConfigurationOptions CreateConnectionMultiplexerOptions(string connectionString)
        {
            ConfigurationOptions options = ConfigurationOptions.Parse(connectionString);
            options.ConnectTimeout = 600000;
            options.AsyncTimeout = 600000;
            options.AllowAdmin = true;
            return options;
        }
    }
}
