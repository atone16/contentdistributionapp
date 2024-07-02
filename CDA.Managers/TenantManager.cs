using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using CDA.IManagers;

namespace CDA.Managers
{
    public class TenantManager : ITenantManager
    {
        private readonly ITenantAccess tenantAccess;
        private readonly IUserAccess userAccess;
        private readonly IContentDistributionAccess contentDistributionAccess;
        private readonly IOrderAccess orderAccess;
        private readonly IBriefAccess briefAccess;
        private readonly IAssetAccess assetAccess;
        private readonly IMapper mapper;

        public TenantManager(
            ITenantAccess tenantAccess,
            IUserAccess userAccess,
            IContentDistributionAccess contentDistributionAccess,
            IOrderAccess orderAccess,
            IBriefAccess briefAccess,
            IAssetAccess assetAccess,
            IMapper mapper)
        {
            this.tenantAccess = tenantAccess;
            this.mapper = mapper;
            this.userAccess = userAccess;
            this.contentDistributionAccess  = contentDistributionAccess;
            this.orderAccess = orderAccess;
            this.briefAccess = briefAccess;
            this.assetAccess = assetAccess;
        }

        public async Task<TenantDto> CreateTenant(TenantInput input)
        {
            var inputTenant = this.mapper.Map<Tenant>(input);
            var createdTenant = await this.tenantAccess.CreateAsync(inputTenant);
            return this.mapper.Map<TenantDto>(createdTenant);
        }

        public async Task<TenantDto> UpdateTenant(Guid id, TenantInput input)
        {
            try
            {
                var tenant = await this.tenantAccess.GetByIdAsync(id);

                if (tenant == null)
                {
                    throw new Exception("Cannot update non existent tenant.");
                }

                if (tenant.IsArchived)
                {
                    throw new Exception("Cannot update an archived tenant.");
                }

                tenant.TenantName = input.TenantName;
                var updatedTenant = await this.tenantAccess.UpdateAsync(tenant);
                return this.mapper.Map<TenantDto>(updatedTenant);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Tenant. {id}. {ex.Message}");
            }
        }

        public async Task<TenantDto> GetById(Guid id)
        {
            var tenant = await this.tenantAccess.GetByIdAsync(id);
            return this.mapper.Map<TenantDto>(tenant);
        }
    }
}
