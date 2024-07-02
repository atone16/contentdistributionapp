using CDA.IManagers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddScoped<IAssetManager, AssetManager>();
            services.AddScoped<ITenantManager, TenantManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IOrderManager, OrderManager>();
            services.AddScoped<IContentDistributionManager, ContentDistributionManager>();
            services.AddScoped<IBriefManager, BriefManager>();

            return services;
        }
    }
}
