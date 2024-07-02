using CDA.IAccess;
using Microsoft.Extensions.DependencyInjection;

namespace CDA.Access
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccess(this IServiceCollection services)
        {
            services.AddScoped<IUserAccess, UserAccess>();
            services.AddScoped<IBriefAccess, BriefAccess>();
            services.AddScoped<IContentDistributionAccess, ContentDistributionAccess>();
            services.AddScoped<IOrderAccess, OrderAccess>();
            services.AddScoped<ITenantAccess, TenantAccess>();
            services.AddScoped<IAssetAccess, AssetAccess>();

            return services;
        }
    }
}
