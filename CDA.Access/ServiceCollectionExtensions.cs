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
            services.AddScoped<IContentDistributionAssetAccess, ContentDistributionAssetAccess>();
            services.AddScoped<IOrderAccess, OrderAccess>();
            services.AddScoped<ITenantAccess, TenantAccess>();
            services.AddScoped<IAssetAccess, AssetAccess>();
            services.AddScoped<IOrderBriefAccess, OrderBriefAccess>();
            services.AddScoped<IBriefCommentAccess , BriefCommentAccess>();
            services.AddScoped<IAssetCommentAccess, AssetCommentAccess>();

            return services;
        }
    }
}
