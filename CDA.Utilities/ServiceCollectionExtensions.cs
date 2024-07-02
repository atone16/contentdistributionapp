using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Utilities
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUtilities(
            this IServiceCollection services)
        {
            services.AddSingleton<IGuidGenerator, GuidGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddAutoMapper(typeof(CDAMapperProfile));
            return services;
        }
    }
}
